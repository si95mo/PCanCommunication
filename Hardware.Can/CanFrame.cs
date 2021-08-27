using System;

namespace Hardware.Can
{
    /// <summary>
    /// Define a basic representation of a can frame
    /// </summary>
    public class CanFrame
    {
        private uint id;
        private byte[] data;
        private double timestamp;

        /// <summary>
        /// The <see cref="CanFrame"/> id
        /// </summary>
        public uint Id => id;

        /// <summary>
        /// The <see cref="CanFrame"/> data
        /// </summary>
        public byte[] Data => data;

        /// <summary>
        /// The <see cref="CanFrame"/> associated timestamp
        /// (in terms of total milliseconds)
        /// </summary>
        public double Timestamp => timestamp;

        /// <summary>
        /// Create a new instance of <see cref="CanFrame"/>
        /// </summary>
        /// <param name="id">The <see cref="CanFrame"/> id</param>
        /// <param name="data">The <see cref="CanFrame"/> data</param>
        /// <param name="timestamp">The <see cref="CanFrame"/> timestamp</param>
        public CanFrame(uint id, byte[] data, double timestamp)
        {
            this.id = id;
            this.data = data;
            this.timestamp = timestamp;
        }

        /// <summary>
        /// Create a new instance of <see cref="CanFrame"/>
        /// </summary>
        /// <param name="id">The <see cref="CanFrame"/> id</param>
        /// <param name="data">The <see cref="CanFrame"/> data</param>
        public CanFrame(uint id, byte[] data) : this(id, data, DateTime.Now.TimeOfDay.TotalMilliseconds)
        { }

        /// <summary>
        /// Return a description of the <see cref="CanFrame"/>
        /// </summary>
        /// <returns>The <see cref="string"/> containing the description</returns>
        public override string ToString()
        {
            string description = $"{timestamp}; {id}; {string.Join(", ", data)}";
            return description;
        }
    }
}