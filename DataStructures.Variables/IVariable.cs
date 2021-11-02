namespace DataStructures.VariablesDictionary
{
    public enum VariableType
    {
        /// <summary>
        /// An integer representation of the <see cref="IVariable"/>
        /// </summary>
        Int = 0,

        /// <summary>
        /// A float representation of the <see cref="IVariable"/>
        /// </summary>
        Sgl = 1
    }

    /// <summary>
    /// Define a general prototype for a variable
    /// </summary>
    public interface IVariable
    {
        /// <summary>
        /// The <see cref="IVariable"/> name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The <see cref="IVariable"/> index
        /// </summary>
        byte Index { get; }

        /// <summary>
        /// The <see cref="IVariable"/> sub index
        /// </summary>
        ushort SubIndex { get; }

        /// <summary>
        /// The <see cref="IVariable"/> variable as <see cref="object"/>
        /// </summary>
        object ValueAsObject { get; set; }

        /// <summary>
        /// The representation of the <see cref="IVariable"/> <see cref="ValueAsObject"/>
        /// </summary>
        VariableType Type { get; set; }
    }

    /// <summary>
    /// Define a general prototype for a variable with a value
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public interface IVariable<T> : IVariable
    {
        /// <summary>
        /// The <see cref="IVariable{T}"/> value
        /// </summary>
        T Value { get; set; }
    }
}