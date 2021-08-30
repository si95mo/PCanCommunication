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

        private ICanResource resource;

        private object objectLock = new object();

        protected EventHandler<DataChangedEventArgs> DataChangedHandler;

        public CanChannel(int canId, ICanResource resource)
        {
            this.canId = canId;
            this.resource = resource;

            data = new byte[8];
            canFrame = new CanFrame(canId, data);
        }

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
        public int CanId
        {
            get => canId;
            set
            {
                canId = value;
                canFrame.Id = canId;
            }
        }

        /// <summary>
        /// The <see cref="CanChannel"/> data
        /// </summary>
        /// <remarks>
        /// Only a change of this property will trigger
        /// the <see cref="DataChanged"/> event!
        /// </remarks>
        public byte[] Data
        {
            get => data;
            set
            {
                // Eventually trigger the value changed event
                // and send the new can frame via the resource
                if (!value.SequenceEqual(data))
                {
                    byte[] oldData = data;
                    data = value;
                    OnDataChanged(new DataChangedEventArgs(oldData, data));

                    canFrame.Data = data;
                    resource.Send(canFrame);
                }
            }
        }

        /// <summary>
        /// The <see cref="CanChannel"/> associated
        /// <see cref="Can.CanFrame"/>
        /// </summary>
        /// <remarks>
        /// A change in this property will not trigger
        /// the <see cref="DataChanged"/> event!
        /// </remarks>
        public CanFrame CanFrame 
        { 
            get => canFrame;
            set
            {
                canFrame = value;
                data = canFrame.Data;
            }
        }

        /// <summary>
        /// Return a textual description of the <see cref="CanChannel"/>
        /// </summary>
        /// <returns>The description</returns>
        public override string ToString()
        {
            string dataAsString = data[0].ToString("D3");
            for (int i = 1; i < data.Length; i++)
                dataAsString += $", {data[i]:D3}";

            string description = $"{canId};\t{dataAsString}";
            return description;
        }

        /// <summary>
        /// Forcibly send a <see cref="Can.CanFrame"/> through
        /// the <see cref="ICanResource"/>
        /// </summary>
        public void Send() => resource.Send(canFrame);
    }
}