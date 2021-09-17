namespace DataStructure.VariablesDictionary
{
    /// <summary>
    /// A <see cref="float"/> type of <see cref="Variable{T}"/>
    /// </summary>
    public class FloatVariable : Variable<double>
    {
        /// <summary>
        /// Create a new instance of <see cref="FloatVariable"/>
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="index">The index</param>
        /// <param name="subIndex">The sub index</param>
        public FloatVariable(string name, uint index, uint subIndex) : base(name, index, subIndex)
        { }

        /// <summary>
        /// Create a new instance of <see cref="FloatVariable"/>
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="index">The index</param>
        /// <param name="subIndex">The sub index</param>
        /// <param name="value">The value</param>
        public FloatVariable(string name, uint index, uint subIndex, double value) : base(name, index, subIndex, value)
        { }
    }
}