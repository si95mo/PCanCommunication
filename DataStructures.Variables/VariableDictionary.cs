using System.Collections.Generic;

namespace DataStructure.VariablesDictionary
{
    /// <summary>
    /// Implement a dictionary containing <see cref="Variable{T}"/>
    /// </summary>
    public static class VariableDictionary
    {
        private static Dictionary<string, IVariable<double>> variables;

        /// <summary>
        /// The <see cref="Dictionary{TKey, TValue}"/> of <see cref="FloatVariable"/>
        /// </summary>
        public static Dictionary<string, IVariable<double>> Variables => variables;

        /// <summary>
        /// Initialize the <see cref="VariableDictionary"/>
        /// </summary>
        public static void Initialize()
        {
            variables = new Dictionary<string, IVariable<double>>();
        }

        /// <summary>
        /// Add a <see cref="Variable{T}"/> to the <see cref="VariableDictionary"/>
        /// </summary>
        /// <param name="variable">The <see cref="IVariable{T}"/> to add</param>
        /// <returns><see langword="true"/> if the variable is added, <see langword="false"/> otherwise</returns>
        public static bool Add(IVariable<double> variable)
        {
            bool added = false;

            if (!variables.ContainsKey(variable.Name))
            {
                variables.Add(variable.Name, variable);
                added = true;
            }

            return added;
        }

        /// <summary>
        /// Get a <see cref="Variable{T}"/> from the <see cref="VariableDictionary"/>
        /// </summary>
        /// <remarks>
        /// If no variable is found, <paramref name="variable"/> will consist of its <see langword="default"/> value
        /// </remarks>
        /// <param name="name">The name of the value to get</param>
        /// <param name="variable">The <see cref="IVariable{T}"/> got</param>
        /// <returns><see langword="true"/> if the variable is found, <see langword="false"/> otherwise</returns>
        public static bool Get(string name, out IVariable<double> variable)
            => variables.TryGetValue(name, out variable);
    }
}