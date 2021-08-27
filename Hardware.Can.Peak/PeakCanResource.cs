using Hardware.Can.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hardware.Can
{
    /// <summary>
    /// Handles the property status changed event.
    /// See also <see cref="EventArgs"/>
    /// </summary>
    public class StatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The old value
        /// </summary>
        public readonly object OldValue;

        /// <summary>
        /// The new value
        /// </summary>
        public readonly object NewValue;

        /// <summary>
        /// Create a new instance of <see cref="StatusChangedEventArgs"/>
        /// </summary>
        /// <param name="oldValue">The old value</param>
        /// <param name="newValue">The new value</param>
        public StatusChangedEventArgs(object oldValue, object newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    /// <summary>
    /// Implements the <see cref="ICanResource"/> interface in order to
    /// communicate with PEAK-CAN hardware
    /// </summary>
    public class PeakCanResource : ICanResource
    {
        private List<ICanChannel> channels;
        private uint status;

        private object objectLock = new object();

        private Task rxTask;
        private bool isReceiving;
        private AutoResetEvent receiveEvent;

        private delegate void ReadHandler();
        private ReadHandler readHandler;

        private ushort channelHandle;

        public EventHandler<StatusChangedEventArgs> StatusChangedHandler;

        /// <summary>
        /// The <see cref="StatusChangedHandler"/> event handler
        /// for the <see cref="Status"/> property
        /// </summary>
        public event EventHandler<StatusChangedEventArgs> StatusChanged
        {
            add
            {
                lock (objectLock)
                    StatusChangedHandler += value;
            }

            remove
            {
                lock (objectLock)
                    StatusChangedHandler -= value;
            }
        }

        /// <summary>
        /// On status changed event
        /// </summary>
        /// <param name="e">The <see cref="StatusChangedEventArgs"/></param>
        protected virtual void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedHandler?.Invoke(this, e);
        }

        /// <summary>
        /// The <see cref="PeakCanResource"/> subscribed
        /// <see cref="ICanChannel"/>
        /// </summary>
        public List<ICanChannel> Channels => channels;

        /// <summary>
        /// The <see cref="PeakCanResource"/> status
        /// </summary>
        public uint Status
        {
            get => status;
            set
            {
                if (value != status)
                {
                    uint oldStatus = status;
                    status = value;
                    OnStatusChanged(new StatusChangedEventArgs(oldStatus, status));
                }
            }
        }

        /// <summary>
        /// Create a new instance of <see cref="PeakCanResource"/>
        /// </summary>
        public PeakCanResource()
        {
            channels = new List<ICanChannel>();

            isReceiving = false;
            receiveEvent = new AutoResetEvent(false);
            readHandler = new ReadHandler(ReadMessages);

            rxTask = default;
        }

        /// <summary>
        /// Create a new instance of <see cref="PeakCanResource"/>. <br/>
        /// See also <see cref="PeakCanResource(ushort, ushort)"/>
        /// </summary>
        /// <param name="hwChannel">The channel handle</param>
        /// <param name="baudRate">The baud rate</param>
        /// <param name="hwType">The hardware type</param>
        /// <param name="ioPort">The IO address of the parallel port</param>
        /// <param name="interrupt">The interrupt number of the parallel port</param>
        public PeakCanResource(ushort hwChannel, ushort baudRate, byte hwType, uint ioPort, ushort interrupt) : this()
        {
            Status = (uint)PCANBasic.Initialize(
                hwChannel, 
                (TPCANBaudrate)baudRate, 
                (TPCANType)hwType, 
                ioPort, 
                interrupt
            );
            channelHandle = hwChannel;
        }

        /// <summary>
        /// Create a new instance of <see cref="PeakCanResource"/>. <br/>
        /// See also <see cref="PeakCanResource(ushort, ushort, byte, uint, ushort)"/>
        /// </summary>
        /// <param name="hwChannel">The channel handle</param>
        /// <param name="baudRate">The baud rate</param>
        public PeakCanResource(ushort hwChannel, ushort baudRate) : this(hwChannel, baudRate, 0, 0, 0)
        { }

        /// <summary>
        /// Start the <see cref="PeakCanResource"/>
        /// </summary>
        public void Start()
        {
            isReceiving = true;

            if (rxTask == default || rxTask.IsCompleted)
            {
                rxTask = CreateReceiveTask();
                rxTask.Start();
            }
        }

        /// <summary>
        /// Stop the <see cref="PeakCanResource"/>
        /// </summary>
        public void Stop()
        {
            isReceiving = false;

            if (rxTask != default && !rxTask.IsCompleted)
            {
                // First, wait for the task to complete the current iteration
                rxTask.Wait();
                rxTask.Dispose();
            }

            rxTask = default;
        }

        /// <summary>
        /// Send a <see cref="CanFrame"/> via the <see cref="PeakCanResource"/>
        /// </summary>
        /// <param name="canFrame">The can frame to send</param>
        /// <returns>
        /// <see langword="true"/> if the operation succeeded,
        /// <see langword="false"/> otherwise
        /// </returns>
        public bool Send(CanFrame canFrame)
        {
            TPCANMsg msg = new TPCANMsg();
            msg.ID = canFrame.Id;
            msg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;
            msg.LEN = 8;
            msg.DATA = canFrame.Data;

            bool succeded = PCANBasic.Write(channelHandle, ref msg) == TPCANStatus.PCAN_ERROR_OK;

            return succeded;
        }

        /// <summary>
        /// Read the can messages and store them 
        /// in the relative <see cref="ICanChannel"/> subscribed
        /// in <see cref="Channels"/>
        /// </summary>
        private void ReadMessages()
        {
            TPCANStatus readResult;
            bool channelFound = false;
            ICanChannel channel;
            CanFrame canFrame;

            do
            {
                readResult = PCANBasic.Read(
                    channelHandle, 
                    out TPCANMsg message, 
                    out TPCANTimestamp t
                );

                for(int i = 0; i < channels.Count && !channelFound; i++)
                {
                    channel = channels.ElementAt(i);
                    if(channel.CanId == message.ID)
                    {
                        channelFound = true;

                        canFrame = new CanFrame(
                            message.ID,
                            message.DATA,
                            (t.micros + 1000 * t.millis + 0x100000000 * 1000 * t.millis_overflow) / 1000
                        );
                        channel.CanFrame = canFrame;
                    }
                }
            } while (isReceiving && !Convert.ToBoolean(readResult & TPCANStatus.PCAN_ERROR_QRCVEMPTY));
        }

        /// <summary>
        /// Create the receiving <see cref="Task"/> for can messages
        /// </summary>
        /// <returns>The receiving <see cref="Task"/></returns>
        private Task CreateReceiveTask()
        {
            Task rx = new Task(() =>
                {
                    uint buffer = Convert.ToUInt32(receiveEvent.SafeWaitHandle.DangerousGetHandle().ToInt32());
                    TPCANStatus setValueResult = PCANBasic.SetValue(
                        channelHandle, 
                        TPCANParameter.PCAN_RECEIVE_EVENT, 
                        ref buffer, 
                        sizeof(uint)
                    );

                    if (setValueResult != TPCANStatus.PCAN_ERROR_OK)
                        Status = (uint)setValueResult;

                    while(isReceiving)
                    {
                        if (receiveEvent.WaitOne(50))
                            readHandler.Invoke();
                    }
                }
            );
            return rx;
        }

        /// <summary>
        /// Set the <see cref="PeakCanResource"/> baud rate
        /// </summary>
        /// <param name="baudRate">The baud rate to set</param>
        /// <returns>
        /// <see langword="true"/> if the operation succeeded,
        /// <see langword="false"/> otherwise
        /// </returns>
        public bool SetBaudRate(uint baudRate)
        {
            throw new NotImplementedException();
        }
    }
}
