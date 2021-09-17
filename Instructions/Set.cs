using DataStructure.VariablesDictionary;
using System.Threading.Tasks;

namespace Instructions
{
    /// <summary>
    /// Implement an <see cref="Instruction"/> for setting a value
    /// </summary>
    public class Set : Instruction
    {
        private string variableName;
        private double valueToSet;

        /// <summary>
        /// Create a new instance of <see cref="Set"/>
        /// </summary>
        /// <param name="variableName">The variable name</param>
        /// <param name="valueToSet">The value to set</param>
        /// <param name="order">The order index</param>
        public Set(string variableName, double valueToSet, int order) : base("Set", order)
        {
            this.variableName = variableName;
            this.valueToSet = valueToSet;

            inputParameters.Add(this.variableName);
            inputParameters.Add(this.valueToSet);
        }

        /// <summary>
        /// Execute the <see cref="Set"/> instruction
        /// </summary>
        public override async Task Execute()
        {
            VariableDictionary.Get(variableName, out IVariable<double> variable);
            variable.Value = valueToSet;
        }
    }
}