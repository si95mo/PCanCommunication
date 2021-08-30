using Hardware.Can.Peak.Lib;
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

        private Queue<CanFrame> logQueue;
        private int maxCapacity;
        private bool logEnabled;

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
            => StatusChangedHandler?.Invoke(this, e);

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
                // Eventually trigger the value changed event
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
        /// and perform an attribute initialization. <br/>
        /// For a full initialization, use <see cref="PeakCanResource(ushort, ushort)"/>
        /// or <see cref="PeakCanResource(ushort, ushort, byte, uint, ushort)"/>
        /// </summary>
        public PeakCanResource()
        {
            channels = new List<ICanChannel>();

            logQueue = null;
            maxCapacity = 65535;
            logEnabled = false;

            isReceiving = false;
            receiveEvent = new AutoResetEvent(false);
            readHandler = new ReadHandler(ReadMessages);

            rxTask = default; // Initialization to a known state
        }

        /// <summary>
        /// Create a new instance of <see cref="PeakCanResource"/>
        /// for non plug-and-play hardware. <br/>
        /// See also <see cref="PeakCanResource(ushort, ushort)"/>
        /// </summary>
        /// <param name="hwChannel">The channel handle</param>
        /// <param name="baudRate">The baud rate</param>
        /// <param name="hwType">The hardware type</param>
        /// <param name="ioPort">The IO address of the parallel port</param>
        /// <param name="interrupt">The interrupt number of the parallel port</param>
        /// <remarks>
        /// This constructor should only be called for the non plug-and-play hardware
        /// (such as PCAN-Dongle or PCAN-ISA). <br/>
        /// For plug-and-play hardware see <see cref="PeakCanResource(ushort, ushort)"/>
        /// </remarks>
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
        /// Create a new instance of <see cref="PeakCanResource"/>
        /// for plug-and-play hardware. <br/>
        /// See also <see cref="PeakCanResource(ushort, ushort, byte, uint, ushort)"/>
        /// </summary>
        /// <param name="hwChannel">The channel handle</param>
        /// <param name="baudRate">The baud rate</param>
        /// <remarks>
        /// This constructor should only be called for the plug-and-play hardware
        /// (such as PCAN-USB, PCAN-USB Pro or PCAN-PCI). <br/>
        /// For non plug-and-play hardware see <see cref="PeakCanResource(ushort, ushort, byte, uint, ushort)"/>
        /// </remarks>
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
            msg.ID = (uint)canFrame.Id;
            msg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;
            msg.LEN = 8;
            msg.DATA = canFrame.Data;

            Status = (uint)PCANBasic.Write(channelHandle, ref msg);
            bool succeeded = status == (uint)TPCANStatus.PCAN_ERROR_OK;

            return succeeded;
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
                canFrame = new CanFrame(
                    (int)message.ID,
                    message.DATA,
                    (t.micros + 1000 * t.millis + 0x100000000 * 1000 * t.millis_overflow) / 1000
                );

                if (logEnabled)
                {
                    if (logQueue.Count >= maxCapacity)
                        logQueue.Dequeue();

                    logQueue.Enqueue(canFrame);
                }

                for (int i = 0; i < channels.Count && !channelFound; i++)
                {
                    channel = channels.ElementAt(i);
                    if (channel.CanId == message.ID)
                    {
                        channelFound = true;
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

                    while (isReceiving)
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

        /// <summary>
        /// Enable the <see cref="PeakCanResource"/> log for
        /// received <see cref="CanFrame"/>
        /// </summary>
        /// <param name="maxLogSize">The maximum log size</param>
        /// <remarks>
        /// Calling multiple <see cref="EnableLog(int)"/> will
        /// delete the previous logged info!
        /// </remarks>
        public void EnableLog(int maxLogSize = 65535)
        {
            logEnabled = true;
            maxCapacity = maxLogSize;
            logQueue = new Queue<CanFrame>(maxLogSize);
        }

        /// <summary>
        /// Disable the <see cref="PeakCanResource"/> log for
        /// received <see cref="CanFrame"/>
        /// </summary>
        /// <remarks>
        /// Calling <see cref="DisableLog"/> will not delete
        /// previous logged info (but calling a second
        /// <see cref="EnableLog(int)"/> will)!
        /// </remarks>
        public void DisableLog() => logEnabled = false;

        /// <summary>
        /// Read the current log for received <see cref="CanFrame"/>. <br/>
        /// See also <see cref="EnableLog(int)"/> and <see cref="DisableLog"/>
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> with the logged info. The <see cref="string"/>
        /// will be empty if there are no logged <see cref="CanFrame"/> <br/>
        /// or <see cref="EnableLog(int)"/> has not been called at least once
        /// </returns>
        public string ReadLog()
        {
            string log = "";

            // EnableLog(int) should has been called at least once
            if (logQueue != null)
            {
                foreach (CanFrame canFrame in logQueue)
                    log += $"{canFrame}{Environment.NewLine}";
            }

            return log;
        }
    }
}