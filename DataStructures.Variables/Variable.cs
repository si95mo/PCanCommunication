using System;

namespace DataStructures.VariablesDictionary
{
    /// <summary>
    /// Handles the <see cref="CanChannel.Data"/> changed event.
    /// See also <see cref="EventArgs"/>
    /// </summary>
    public class ValueChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The old data
        /// </summary>
        public readonly object OldValue;

        /// <summary>
        /// The new data
        /// </summary>
        public readonly object NewValue;

        /// <summary>
        /// Create a new instance of <see cref="ValueChangedEventArgs"/>
        /// </summary>
        /// <param name="oldValue">The old value of the property</param>
        /// <param name="newValue">The new value of the property</param>
        public ValueChangedEventArgs(object oldValue, object newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    /// <summary>
    /// Implement a basic variable. See <see cref="IVariable{T}"/>
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public class Variable<T> : IVariable<T>
    {
        protected string name;
        protected uint index;
        protected uint subIndex;
        protected T value;
        protected string description;

        private object lockObject;

        /// <summary>
        /// The <see cref="Variable{T}"/> name
        /// </summary>
        public string Name => name;

        /// <summary>
        /// The <see cref="Variable{T}"/> index
        /// </summary>
        public uint Index => index;

        /// <summary>
        /// The <see cref="Variable{T}"/> sub index
        /// </summary>
        public uint SubIndex => subIndex;

        /// <summary>
        /// The <see cref="Variable{T}"/> value
        /// </summary>
        public T Value
        {
            get => value;
            set
            {
                T oldValue = this.value;
                this.value = value;

                OnValueChanged(new ValueChangedEventArgs(oldValue, this.value));
            }
        }

        /// <summary>
        /// The <see cref="Variable{T}"/> type, same as <see langword="typeof"/>(<see cref="T"/>)
        /// </summary>
        public Type Type => typeof(T);

        /// <summary>
        /// The <see cref="Variable{T}"/> description
        /// </summary>
        public string Description => description;

        /// <summary>
        /// The <see cref="ValueChanged"/> handler
        /// </summary>
        protected EventHandler<ValueChangedEventArgs> ValueChangedHandler;

        /// <summary>
        /// The <see cref="ValueChanged"/> event handler
        /// for the <see cref="Value"/> property
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ValueChanged
        {
            add
            {
                lock (lockObject)
                    ValueChangedHandler += value;
            }

            remove
            {
                lock (lockObject)
                    ValueChangedHandler -= value;
            }
        }

        /// <summary>
        /// Initialize the <see cref="Variable{T}"/> instance attributes
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="index">The index</param>
        /// <param name="subIndex">The sub index</param>
        /// <param name="description">The description</param>
        protected Variable(string name, uint index, uint subIndex, string description = "") : this(name, index, subIndex, default, description)
        { }

        /// <summary>
        /// Initialize <see cref="Variable{T}"/> instance attributes
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="index">The index</param>
        /// <param name="subIndex">THe sub index</param>
        /// <param name="value">The value</param>
        /// <param name="description">The description</param>
        protected Variable(string name, uint index, uint subIndex, T value, string description = "")
        {
            this.name = name;
            this.index = index;
            this.subIndex = subIndex;
            this.value = value;
            this.description = description;

            lockObject = new object();
        }

        /// <summary>
        /// On <see cref="Value"/> changed event
        /// </summary>
        /// <param name="e">The <see cref="ValueChangedEventArgs"/></param>
        protected virtual void OnValueChanged(ValueChangedEventArgs e)
            => ValueChangedHandler?.Invoke(this, e);
    }
}