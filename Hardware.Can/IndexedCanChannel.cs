using System;
using System.Linq;

namespace Hardware.Can
{
    /// <summary>
    /// Define a channel to handle the can communication
    /// with an indexed <see cref="CanFrame"/>. <br/>
    /// The first 2 bytes represent the index, the second 2
    /// the sub-index and the last 4 the actual data. <br/>
    /// See also <see cref="CanChannel"/>
    /// </summary>
    public class IndexedCanChannel : CanChannel
    {
        private byte cmd;
        private byte index;
        private ushort subIndex;

        /// <summary>
        /// The <see cref="IndexedCanChannel"/> cmd
        /// </summary>
        /// <remarks>0 to receive, 1 to transmit</remarks>
        public byte Cmd
        {
            get => cmd;
            set => cmd = value;
        }

        /// <summary>
        /// The <see cref="IndexedCanChannel"/> index
        /// </summary>
        public byte Index
        {
            get => index;
            set => index = value;
        }

        /// <summary>
        /// The <see cref="IndexedCanChannel"/> sub index
        /// </summary>
        public ushort SubIndex
        {
            get => subIndex;
            set => subIndex = value;
        }

        /// <summary>
        /// The <see cref="IndexedCanChannel"/> data
        /// </summary>
        /// <remarks>
        /// <see cref="Data"/> is an 8 <see cref="byte"/>
        /// long array, but the first 4 elements are
        /// <see cref="Cmd"/>, <see cref="Index"/> and <see cref="SubIndex"/>, <br/>
        /// while the actual data are stored in the last 4 elements.
        /// So, the array passed in the setter must have a length of 4! <br/>
        /// <b>This property is used for writing to the can bus</b>
        /// </remarks>
        public override byte[] Data
        {
            get => data;
            set
            {
                // Here the prepend logic
                byte[] cmdAsArray = new byte[] { cmd }; // Should be 1 byte
                byte[] indexAsArray = new byte[] { index }; // Should be 1 byte
                byte[] subIndexAsArray = BitConverter.GetBytes(subIndex); // Should be 2 bytes
                byte[] firstHalf = new byte[cmdAsArray.Length + indexAsArray.Length + subIndexAsArray.Length]; // Should be 4 bytes
                cmdAsArray.CopyTo(firstHalf, 0); // From 0
                indexAsArray.CopyTo(firstHalf, cmdAsArray.Length); // From 1
                subIndexAsArray.CopyTo(firstHalf, cmdAsArray.Length + indexAsArray.Length); // From 2, 4 bytes in total

                // Here the old data reconstruction
                byte[] oldData = new byte[8];
                firstHalf.CopyTo(oldData, 0);

                byte[] secondHalf = new byte[4];
                Array.Copy(data, 4, secondHalf, 0, 4);
                secondHalf.CopyTo(oldData, firstHalf.Length);

                // Here the data update
                data = firstHalf.Concat(value).ToArray(); // Should be 4 bytes + 4 bytes = 8 bytes
                OnDataChanged(new DataChangedEventArgs(oldData, data));

                CanFrame = new CanFrame(canId, data); // Trigger the event
                resource.Send(canFrame);
            }
        }

        /// <summary>
        /// The <see cref="Can.CanFrame"/>
        /// </summary>
        /// <remarks>
        /// <see cref="CanFrame.Data"/> must be an 8 <see cref="byte"/>
        /// long array (the first 4 elements are <see cref="Cmd"/>
        /// <see cref="Index"/> and <see cref="SubIndex"/>, the last 4
        /// the <see cref="CanFrame.Data"/>)! <br/>
        /// <b>This property is used for reading from the can bus</b>
        /// </remarks>
        public override CanFrame CanFrame
        {
            get => canFrame;
            set
            {
                // Here the index-subIndex acquisition logic
                CanFrame oldCanFrame = canFrame;
                canFrame = value;
                OnCanFrameChanged(new CanFrameChangedEventArgs(oldCanFrame, canFrame));

                cmd = canFrame.Data[0];
                index = canFrame.Data[1];
                subIndex = BitConverter.ToUInt16(canFrame.Data, 2);
                Array.Copy(canFrame.Data, 4, data, 4, 4);
            }
        }

        /// <summary>
        /// Create a new instance of <see cref="IndexedCanChannel"/>. <br/>
        /// See also <see cref="CanChannel"/>
        /// </summary>
        /// <param name="canId">The can id</param>
        /// <param name="index">The can channel index</param>
        /// <param name="subIndex">The can channel sub index</param>
        /// <param name="resource">The <see cref="ICanResource"/></param>
        /// <param name="cmd">The cmd value, 0 to receive and 1 to transmit</param>
        public IndexedCanChannel(int canId, byte index, ushort subIndex, ICanResource resource, byte cmd = 0)
            : base(canId, resource)
        {
            this.index = index;
            this.subIndex = subIndex;
            this.cmd = cmd;

            resource.Channels.Add(this);
        }
    }
}