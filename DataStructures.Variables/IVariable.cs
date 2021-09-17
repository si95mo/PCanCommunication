namespace DataStructures.VariablesDictionary
{
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
        uint Index { get; }

        /// <summary>
        /// The <see cref="IVariable"/> sub index
        /// </summary>
        uint SubIndex { get; }
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