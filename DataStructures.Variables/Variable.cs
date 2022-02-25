using Hardware.Can;
using System;

namespace DataStructures.VariablesDictionary
{
    /// <summary>
    /// Handles the <see cref="Variable{T}.Value"/> changed event.
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
        protected byte index;
        protected ushort subIndex;
        protected T value;
        protected string description;
        protected VariableType type;
        protected double scale, offset;
        protected string measureUnit;

        private object lockObject;

        /// <summary>
        /// The <see cref="Variable{T}"/> name
        /// </summary>
        public string Name => name;

        /// <summary>
        /// The <see cref="Variable{T}"/> index
        /// </summary>
        public byte Index => index;

        /// <summary>
        /// The <see cref="Variable{T}"/> sub index
        /// </summary>
        public ushort SubIndex => subIndex;

        public VariableType Type { get => type; set => type = value; }

        /// <summary>
        /// The <see cref="Variable{T}"/> value.
        /// Also handle the offset and scaling
        /// </summary>
        public T Value
        {
            get
            {
                dynamic a = value;
                dynamic b = scale;
                dynamic c = offset;

                return (T)(a * b + c);
            }
            set
            {
                dynamic a = value;
                dynamic b = scale;
                dynamic c = offset;

                T oldValue = this.value;
                this.value = (T)((a - c) / b);

                OnValueChanged(new ValueChangedEventArgs(oldValue, this.value));
            }
        }

        /// <summary>
        /// The <see cref="Variable{T}.Value"/> as <see cref="object"/>
        /// </summary>
        public object ValueAsObject
        {
            get
            {
                object valueAsObject = Value;

                if (type == VariableType.Int)
                    valueAsObject = Convert.ToInt32(valueAsObject);
                else
                    valueAsObject = Convert.ToSingle(valueAsObject);

                return valueAsObject;
            }

            set => Value = (T)value;
        }

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
        protected Variable(string name, byte index, ushort subIndex, VariableType type,
            double scale = 1d, double offset = 0d, string description = "")
            : this(name, index, subIndex, default, type, scale, offset, description)
        { }

        /// <summary>
        /// Initialize <see cref="Variable{T}"/> instance attributes
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="index">The index</param>
        /// <param name="subIndex">THe sub index</param>
        /// <param name="value">The value</param>
        /// <param name="description">The description</param>
        protected Variable(string name, byte index, ushort subIndex, T value,
            VariableType type, double scale = 1d, double offset = 0d, string description = "")
        {
            this.name = name;
            this.index = index;
            this.subIndex = subIndex;
            this.value = value;
            this.type = type;
            this.scale = scale;
            this.offset = offset;
            this.description = description;

            lockObject = new object();
        }

        /// <summary>
        /// On <see cref="Value"/> changed event
        /// </summary>
        /// <param name="e">The <see cref="ValueChangedEventArgs"/></param>
        protected virtual void OnValueChanged(ValueChangedEventArgs e)
            => ValueChangedHandler?.Invoke(this, e);

        /// <summary>
        /// Update the <see cref="IndexedCanChannel"/> with the <see cref="Variable{T}.value"/>
        /// </summary>
        /// <param name="tx"></param>
        public void UpdateIndexedCanChannel(IndexedCanChannel tx)
        {
            tx.Index = Index;
            tx.SubIndex = SubIndex;

            // Get the byte array from the variable value
            byte[] data = Type == VariableType.Int ? BitConverter.GetBytes(Convert.ToInt32(Value))
                : BitConverter.GetBytes(Convert.ToSingle(Value));

            tx.Data = data;
        }

        public override string ToString()
        {
            string description = $"{name}, ";

            if (description.CompareTo("") != 0)
                description += $"({Description}), ";

            description += $"{value}";

            return description;
        }
    }
}