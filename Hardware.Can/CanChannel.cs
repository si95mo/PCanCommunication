using System;
using System.Linq;

namespace Hardware.Can
{
    /// <summary>
    /// Handles the <see cref="CanChannel.Data"/> changed event.
    /// See also <see cref="EventArgs"/>
    /// </summary>
    public class DataChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The old data
        /// </summary>
        public readonly object OldData;

        /// <summary>
        /// The new data
        /// </summary>
        public readonly object NewData;

        /// <summary>
        /// Create a new instance of <see cref="DataChangedEventArgs"/>
        /// </summary>
        /// <param name="oldValue">The old value of the property</param>
        /// <param name="newValue">The new value of the property</param>
        public DataChangedEventArgs(object oldValue, object newValue)
        {
            OldData = oldValue;
            NewData = newValue;
        }
    }

    /// <summary>
    /// Handles the <see cref="CanChannel.CanFrame"/> changed event.
    /// See also <see cref="EventArgs"/>
    /// </summary>
    public class CanFrameChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The old <see cref="CanFrame"/>
        /// </summary>
        public readonly object OldCanFrame;

        /// <summary>
        /// The new <see cref="CanFrame"/>
        /// </summary>
        public readonly object NewCanFrame;

        /// <summary>
        /// Create a new instance of <see cref="CanFrameChangedEventArgs"/>
        /// </summary>
        /// <param name="oldCanFrame">The old value of the property</param>
        /// <param name="newCanFrame">The new value of the property</param>
        public CanFrameChangedEventArgs(object oldCanFrame, object newCanFrame)
        {
            OldCanFrame = oldCanFrame;
            NewCanFrame = newCanFrame;
        }
    }

    /// <summary>
    /// Define a channel to handle the can communication. <br/>
    /// See also <see cref="ICanResource"/> for the protocol handling
    /// </summary>
    public class CanChannel : ICanChannel
    {
        /// <summary>
        /// The can id
        /// </summary>
        protected int canId;

        /// <summary>
        /// The data
        /// </summary>
        protected byte[] data;

        /// <summary>
        /// The can frame
        /// </summary>
        protected CanFrame canFrame;

        /// <summary>
        /// The can resource
        /// </summary>
        protected ICanResource resource;

        private object objectLock = new object();

        /// <summary>
        /// The <see cref="DataChanged"/> handler
        /// </summary>
        protected EventHandler<DataChangedEventArgs> DataChangedHandler;

        /// <summary>
        /// The <see cref="CanFrameChanged"/> handler
        /// </summary>
        protected EventHandler<CanFrameChangedEventArgs> CanFrameChangedHandler;

        /// <summary>
        /// Create a new instance of <see cref="CanChannel"/>
        /// </summary>
        /// <param name="canId">The can id</param>
        /// <param name="resource">The <see cref="ICanResource"/></param>
        public CanChannel(int canId, ICanResource resource)
        {
            this.canId = canId;
            this.resource = resource;

            data = new byte[8];
            canFrame = new CanFrame(canId, data);
        }

        /// <summary>
        /// The <see cref="DataChanged"/> event handler
        /// for the <see cref="Data"/> property
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
        /// The <see cref="CanFrameChanged"/> event handler
        /// for the <see cref="CanFrame"/> property
        /// </summary>
        public event EventHandler<CanFrameChangedEventArgs> CanFrameChanged
        {
            add
            {
                lock (objectLock)
                    CanFrameChangedHandler += value;
            }

            remove
            {
                lock (objectLock)
                    CanFrameChangedHandler -= value;
            }
        }

        /// <summary>
        /// On <see cref="Data"/> changed event
        /// </summary>
        /// <param name="e">The <see cref="DataChangedEventArgs"/></param>
        protected virtual void OnDataChanged(DataChangedEventArgs e)
            => DataChangedHandler?.Invoke(this, e);

        /// <summary>
        /// On <see cref="CanFrame"/> changed event
        /// </summary>
        /// <param name="e">The <see cref="CanFrameChangedEventArgs"/></param>
        protected virtual void OnCanFrameChanged(CanFrameChangedEventArgs e)
            => CanFrameChangedHandler?.Invoke(this, e);

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
        /// the <see cref="DataChanged"/> event! <br/>
        /// So, <b>this property is used for writing to the can bus</b>
        /// </remarks>
        public virtual byte[] Data
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

                    CanFrame = new CanFrame(canId, data);
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
        /// the <see cref="DataChanged"/> event! <br/>
        /// So, <b>this property is used for reading from the can bus</b>
        /// </remarks>
        public virtual CanFrame CanFrame
        {
            get => canFrame;
            set
            {
                CanFrame oldCanFrame = canFrame;
                canFrame = value;
                OnCanFrameChanged(new CanFrameChangedEventArgs(oldCanFrame, canFrame));

                data = canFrame.Data;
            }
        }

        /// <summary>
        /// Return a textual description of the <see cref="CanChannel"/>
        /// </summary>
        /// <returns>The description</returns>
        public override string ToString() => canFrame.ToString();

        /// <summary>
        /// Forcibly send a <see cref="Can.CanFrame"/> through
        /// the <see cref="ICanResource"/>
        /// </summary>
        public void Send() => resource.Send(canFrame);
    }
}