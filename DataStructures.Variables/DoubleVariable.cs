namespace DataStructures.VariablesDictionary
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
        public DoubleVariable(string name, byte index, ushort subIndex, VariableType type, string description,
            string measureUnit, double scale = 1d, double offset = 0d)
            : base(name, index, subIndex, type, scale, offset, description)
        {
            this.scale = scale;
            this.offset = offset;
            this.measureUnit = measureUnit;
        }

        /// <summary>
        /// Create a new instance of <see cref="DoubleVariable"/>
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="index">The index</param>
        /// <param name="subIndex">The sub index</param>
        /// <param name="value">The value</param>
        public DoubleVariable(string name, byte index, ushort subIndex, double value,
            VariableType type, string description, string measureUnit, double scale = 1d, double offset = 0d)
            : base(name, index, subIndex, value, type, scale, offset, description)
        {
            this.scale = scale;
            this.offset = offset;
            this.description = description;
            this.measureUnit = measureUnit;
        }
    }
}