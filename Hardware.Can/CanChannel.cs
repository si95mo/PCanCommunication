using System;
using System.Linq;

namespace Hardware.Can
{
    /// <summary>
    /// Handles the property data changed event.
    /// See also <see cref="EventArgs"/>
    /// </summary>
    public class DataChangedEventArgs : EventArgs
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
        /// Create a new instance of <see cref="DataChangedEventArgs"/>
        /// </summary>
        /// <param name="oldValue">The old value of the property</param>
        /// <param name="newValue">The new value of the property</param>
        public DataChangedEventArgs(object oldValue, object newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    /// <summary>
    /// Define a channel to handle the can communication. <br/>
    /// See also <see cref="ICanResource"/> for the protocol handling
    /// </summary>
    public class CanChannel : ICanChannel
    {
        private int canId;
        private byte[] data;
        private CanFrame canFrame;

        private object objectLock = new object();

        protected EventHandler<DataChangedEventArgs> DataChangedHandler;

        /// <summary>
        /// The <see cref="DataChanged"/> event handler
        /// for the <see cref="Value"/> property
        /// </summary>
        public event EventHandler<DataChangedEventArgs> DataChanged
        {
            add
            {
                lock (objectLock)
                    DataChangedHandler += value;
            }

            remove
            {
                lock (objectLock)
                    DataChangedHandler -= value;
            }
        }

        /// <summary>
        /// On value changed event
        /// </summary>
        /// <param name="e">The <see cref="ValueChangedEventArgs"/></param>
        protected virtual void OnDataChanged(DataChangedEventArgs e)
        {
            DataChangedHandler?.Invoke(this, e);
        }

        /// <summary>
        /// The <see cref="CanChannel"/> can id
        /// </summary>
        public int CanId { get => canId; set => canId = value; }

        /// <summary>
        /// The <see cref="CanChannel"/> data
        /// </summary>
        public byte[] Data
        {
            get => data;
            set
            {
                // Eventually trigger the value changed event
                if (!value.SequenceEqual(data))
                {
                    byte[] oldData = data;
                    data = value;
                    OnDataChanged(new DataChangedEventArgs(oldData, data));
                }
            }
        }

        /// <summary>
        /// The <see cref="CanChannel"/> associated
        /// <see cref="Can.CanFrame"/>
        /// </summary>
        public CanFrame CanFrame
        {
            get => canFrame;
            set
            {
                canFrame = value;
                Data = canFrame.Data; // value changed event trigger (if needed)
            }
        }
    }
}