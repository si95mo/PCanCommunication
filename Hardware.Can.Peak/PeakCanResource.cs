using Hardware.Can.Peak.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hardware.Can
{
    /// <summary>
    /// Define the available <see cref="PeakCanResource"/>
    /// baud-rates (bit-rates)
    /// </summary>
    public enum BaudRate : ushort
    { 
        /// <summary>
        /// 1 MBit/s
        /// </summary>
        K1000 = 0x0014,

        /// <summary>
        /// 800 KBit/s
        /// </summary>
        K800 = 0x0016,

        /// <summary>
        /// 500 kBit/s
        /// </summary>
        K500 = 0x001C,

        /// <summary>
        /// 250 kBit/s
        /// </summary>
        K250 = 0x011C,

        /// <summary>
        /// 125 kBit/s
        /// </summary>
        K125 = 0x031C,

        /// <summary>
        /// 100 kBit/s
        /// </summary>
        K100 = 0x432F,

        /// <summary>
        /// 95,238 KBit/s
        /// </summary>
        K95 = 0xC34E,

        /// <summary>
        /// 83,333 KBit/s
        /// </summary>
        K83 = 0x852B,

        /// <summary>
        /// 50 kBit/s
        /// </summary>
        K50 = 0x472F,

        /// <summary>
        /// 47,619 KBit/s
        /// </summary>
        K47 = 0x1414,

        /// <summary>
        /// 33,333 KBit/s
        /// </summary>
        K33 = 0x8B2F,

        /// <summary>
        /// 20 kBit/s
        /// </summary>
        K20 = 0x532F,

        /// <summary>
        /// 10 kBit/s
        /// </summary>
        K10 = 0x672F,

        /// <summary>
        /// 5 kBit/s
        /// </summary>
        K5 = 0x7F7F,
    }

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
        private object logLock = new object();

        private Task rxTask;
        private bool started;
        private AutoResetEvent receiveEvent;

        private delegate void ReadHandler();

        private ReadHandler readHandler;

        private ushort channelHandle;
        private byte hwType;
        private uint ioPort;
        private ushort interrupt;

        private Dictionary<int, bool> filteredCanId;

        /// <summary>
        /// The <see cref="Dictionary{TKey, TValue}"/> with
        /// the filtered can id
        /// </summary>
        public Dictionary<int, bool> FilteredCanId => filteredCanId;

        /// <summary>
        /// The <see cref="StatusChanged"/> handler
        /// </summary>
        protected EventHandler<StatusChangedEventArgs> StatusChangedHandler;

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
        /// For a full initialization, use <see cref="PeakCanResource(ushort, BaudRate)"/>
        /// or <see cref="PeakCanResource(ushort, BaudRate, byte, uint, ushort)"/>
        /// </summary>
        public PeakCanResource()
        {
            channels = new List<ICanChannel>();

            logQueue = null;
            maxCapacity = 65535;
            logEnabled = false;

            started = false;
            receiveEvent = new AutoResetEvent(false);
            readHandler = new ReadHandler(ReadMessages);

            rxTask = default; // Initialization to a known state

            filteredCanId = new Dictionary<int, bool>();
        }

        /// <summary>
        /// Create a new instance of <see cref="PeakCanResource"/>
        /// for non plug-and-play hardware. <br/>
        /// See also <see cref="PeakCanResource(ushort, BaudRate)"/>
        /// </summary>
        /// <param name="hwChannel">The channel handle</param>
        /// <param name="baudRate">The <see cref="BaudRate"/></param>
        /// <param name="hwType">The hardware type</param>
        /// <param name="ioPort">The IO address of the parallel port</param>
        /// <param name="interrupt">The interrupt number of the parallel port</param>
        /// <remarks>
        /// This constructor should only be called for the non plug-and-play hardware
        /// (such as PCAN-Dongle or PCAN-ISA). <br/>
        /// For plug-and-play hardware see <see cref="PeakCanResource(ushort, BaudRate)"/>
        /// </remarks>
        public PeakCanResource(ushort hwChannel, BaudRate baudRate, byte hwType, uint ioPort, ushort interrupt) : this()
        {
            Status = (uint)PCANBasic.Initialize(
                hwChannel,
                (TPCANBaudrate)baudRate,
                (TPCANType)hwType,
                ioPort,
                interrupt
            );

            channelHandle = hwChannel;
            this.hwType = hwType;
            this.ioPort = ioPort;
            this.interrupt = interrupt;
        }

        /// <summary>
        /// Create a new instance of <see cref="PeakCanResource"/>
        /// for plug-and-play hardware. <br/>
        /// See also <see cref="PeakCanResource(ushort, BaudRate, byte, uint, ushort)"/>
        /// </summary>
        /// <param name="hwChannel">The channel handle</param>
        /// <param name="baudRate">The <see cref="BaudRate"/></param>
        /// <remarks>
        /// This constructor should only be called for the plug-and-play hardware
        /// (such as PCAN-USB, PCAN-USB Pro or PCAN-PCI). <br/>
        /// For non plug-and-play hardware see <see cref="PeakCanResource(ushort, BaudRate, byte, uint, ushort)"/>
        /// </remarks>
        public PeakCanResource(ushort hwChannel, BaudRate baudRate) : this(hwChannel, baudRate, 0, 0, 0)
        { }

        /// <summary>
        /// Start the <see cref="PeakCanResource"/>
        /// </summary>
        public void Start()
        {
            started = true;

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
            started = false;

            if (rxTask != default && !rxTask.IsCompleted)
                rxTask.Wait(100);

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
            bool succeeded = true;

            if (started)
            {
                TPCANMsg msg = new TPCANMsg();
                msg.ID = (uint)canFrame.Id;
                msg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;
                msg.LEN = 8;
                msg.DATA = canFrame.Data;

                Status = (uint)PCANBasic.Write(channelHandle, ref msg);
                succeeded = status == (uint)TPCANStatus.PCAN_ERROR_OK;
            }

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
                    if (filteredCanId.TryGetValue(canFrame.Id, out bool enabled))
                    {
                        if (enabled)
                        {
                            lock (logLock)
                            {
                                if (logQueue.Count >= maxCapacity)
                                    logQueue.Dequeue();

                                logQueue.Enqueue(canFrame);
                            }
                        }
                    }
                }

                for (int i = 0; i < channels.Count && !channelFound; i++)
                {
                    channel = channels.ElementAt(i);
                    if (channel.CanId == message.ID)
                    {
                        channelFound = true;
                        channel.Data = canFrame.Data;
                        channel.CanFrame = canFrame;
                    }
                }
            } while (started && !Convert.ToBoolean(readResult & TPCANStatus.PCAN_ERROR_QRCVEMPTY));
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

                    while (started)
                    {
                        if (receiveEvent.WaitOne(10))
                            readHandler.Invoke();
                    }
                }
            );
            return rx;
        }

        /// <summary>
        /// Set the <see cref="PeakCanResource"/> baud rate
        /// </summary>
        /// <param name="baudRate">The <see cref="BaudRate"/> to set</param>
        /// <returns>
        /// <see langword="true"/> if the operation succeeded,
        /// <see langword="false"/> otherwise
        /// </returns>
        public bool SetBaudRate(BaudRate baudRate)
        {
            bool succeeded = true;

            Status = (uint)PCANBasic.Uninitialize(channelHandle);
            Status = (uint)PCANBasic.Initialize(channelHandle, (TPCANBaudrate)baudRate, (TPCANType)hwType, ioPort, interrupt);

            succeeded &= status == (uint)TPCANStatus.PCAN_ERROR_OK;

            return succeeded;
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
        /// Read the current log of the received <see cref="CanFrame"/>
        /// and then empty the log queue. <br/>
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
                lock (logLock)
                {
                    foreach (CanFrame canFrame in logQueue)
                        log += $"{canFrame}{Environment.NewLine}";

                    logQueue.Clear();
                }
            }

            return log;
        }

        /// <summary>
        /// Add or enable a can id into the filtered collection
        /// </summary>
        /// <param name="canId">The can id to add</param>
        public void AddFilteredCanId(int canId)
        {
            if (filteredCanId.ContainsKey(canId))
                filteredCanId[canId] = true;
            else
                filteredCanId.Add(canId, true);
        }

        /// <summary>
        /// Remove or disable a can id into the filtered collection
        /// </summary>
        /// <param name="canId">The can id to remove</param>
        public void RemoveFilteredCanId(int canId)
        {
            if (filteredCanId.ContainsKey(canId))
                filteredCanId.Remove(canId);
            else
                filteredCanId.Add(canId, false);
        }

        /// <summary>
        /// Get all the available hardware
        /// </summary>
        /// <returns>The available hardware handle and its name</returns>
        public static List<string> GetAvailableHardware()
        {
            List<string> hardwareNames = new List<string>();

            TPCANStatus result = PCANBasic.GetValue(
                PCANBasic.PCAN_NONEBUS, 
                TPCANParameter.PCAN_ATTACHED_CHANNELS_COUNT, 
                out uint channelsNumber, 
                sizeof(uint)
            );

            if (result == TPCANStatus.PCAN_ERROR_OK)
            {
                TPCANChannelInformation[] info = new TPCANChannelInformation[channelsNumber];

                result = PCANBasic.GetValue(
                    PCANBasic.PCAN_NONEBUS, 
                    TPCANParameter.PCAN_ATTACHED_CHANNELS, 
                    info
                );

                if (result == TPCANStatus.PCAN_ERROR_OK)
                {
                    uint channelAvailable;
                    foreach (TPCANChannelInformation channel in info)
                    {
                        channelAvailable = channel.channel_condition & PCANBasic.PCAN_CHANNEL_AVAILABLE;

                        if (channelAvailable == PCANBasic.PCAN_CHANNEL_AVAILABLE)
                            hardwareNames.Add(FormatChannelName(channel.channel_handle));
                    }
                }
            }

            return hardwareNames;
        }

        /// <summary>
        /// Gets the formated text for a PCAN-Basic channel handle
        /// </summary>
        /// <param name="handle">PCAN-Basic Handle to format</param>
        /// <returns>The formatted text for a channel</returns>
        private static string FormatChannelName(uint handle)
        {
            TPCANDevice devDevice;
            byte byChannel;

            // Gets the owner device and channel for a 
            // PCAN-Basic handle
            //
            if (handle < 0x100)
            {
                devDevice = (TPCANDevice)(handle >> 4);
                byChannel = (byte)(handle & 0xF);
            }
            else
            {
                devDevice = (TPCANDevice)(handle >> 8);
                byChannel = (byte)(handle & 0xFF);
            }

            return string.Format("{0} {1} ({2:X2}h)", devDevice, byChannel, handle);
        }
    }
}