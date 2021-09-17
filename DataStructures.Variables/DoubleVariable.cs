﻿namespace DataStructures.VariablesDictionary
{
    /// <summary>
    /// A <see cref="double"/> type of <see cref="Variable{T}"/>
    /// </summary>
    public class DoubleVariable : Variable<double>
    {
        /// <summary>
        /// Create a new instance of <see cref="DoubleVariable"/>
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="index">The index</param>
        /// <param name="subIndex">The sub index</param>
        public DoubleVariable(string name, uint index, uint subIndex) : base(name, index, subIndex)
        { }

        /// <summary>
        /// Create a new instance of <see cref="DoubleVariable"/>
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="index">The index</param>
        /// <param name="subIndex">The sub index</param>
        /// <param name="value">The value</param>
        public DoubleVariable(string name, uint index, uint subIndex, double value) : base(name, index, subIndex, value)
        { }
    }
}