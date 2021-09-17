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
        private ushort index;
        private ushort subIndex;

        /// <summary>
        /// The <see cref="IndexedCanChannel"/> index
        /// </summary>
        public ushort Index
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
        /// <see cref="Index"/> and <see cref="SubIndex"/>), <br/>
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
                byte[] indexAsArray = BitConverter.GetBytes(index); // Should be 2 bytes
                byte[] subIndexAsArray = BitConverter.GetBytes(subIndex); // Should be 2 bytes
                byte[] firstHalf = new byte[indexAsArray.Length + subIndexAsArray.Length]; // Should be 4 bytes
                indexAsArray.CopyTo(firstHalf, 0);
                subIndexAsArray.CopyTo(firstHalf, indexAsArray.Length);
                byte[] oldData = new byte[8];
                firstHalf.CopyTo(oldData, 0);
                data.CopyTo(oldData, firstHalf.Length);

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
        /// long array (the first 4 elements are
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

                index = BitConverter.ToUInt16(canFrame.Data, 0);
                subIndex = BitConverter.ToUInt16(canFrame.Data, 2);
                Array.Copy(canFrame.Data, 4, data, 0, 4);
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
        public IndexedCanChannel(int canId, ushort index, ushort subIndex, ICanResource resource)
            : base(canId, resource)
        {
            this.index = index;
            this.subIndex = subIndex;
        }
    }
}