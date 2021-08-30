namespace Hardware.Can
{
    /// <summary>
    /// Define a basic prototype for a can channel
    /// </summary>
    public interface ICanChannel
    {
        /// <summary>
        /// The <see cref="ICanChannel"/> can id
        /// </summary>
        int CanId { get; set; }

        /// <summary>
        /// The <see cref="ICanChannel"/> data
        /// </summary>
        byte[] Data { get; set; }

        /// <summary>
        /// The <see cref="ICanChannel"/> associated
        /// <see cref="Can.CanFrame"/>
        /// </summary>
        CanFrame CanFrame { get; set; }

        /// <summary>
        /// Forcibly send a <see cref="Can.CanFrame"/> through
        /// the <see cref="ICanResource"/>
        /// </summary>
        void Send();
    }
}