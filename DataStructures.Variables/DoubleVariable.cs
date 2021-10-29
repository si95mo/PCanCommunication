namespace DataStructures.VariablesDictionary
{
    /// <summary>
    /// A <see cref="double"/> type of <see cref="Variable{T}"/>
    /// </summary>
    public class DoubleVariable : Variable<double>
    {
        double scale, offset;
        string measureUnit;

        /// <summary>
        /// Create a new instance of <see cref="DoubleVariable"/>
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="index">The index</param>
        /// <param name="subIndex">The sub index</param>
        public DoubleVariable(string name, uint index, uint subIndex, VariableType type, string description, 
            string measureUnit, double scale = 1d, double offset = 0d) 
            : base(name, index, subIndex, type, description)
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
        public DoubleVariable(string name, uint index, uint subIndex, double value,
            VariableType type, string description, string measureUnit, double scale = 1d, double offset = 0d) 
            : base(name, index, subIndex, value, type)
        {
            this.scale = scale;
            this.offset = offset;
            this.measureUnit = measureUnit;
        }
    }
}