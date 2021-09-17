using DataStructures.VariablesDictionary;
using System.Threading.Tasks;

namespace Instructions
{
    /// <summary>
    /// Implement an <see cref="Instruction"/> for getting a value
    /// </summary>
    public class Get : Instruction
    {
        protected string variableName;
        protected double valueGot;

        /// <summary>
        /// Create a new instance of <see cref="Get"/>
        /// </summary>
        /// <param name="variableName">The variable name</param>
        /// <param name="order">The order index</param>
        public Get(string variableName, int order) : base("Get", order)
        {
            this.variableName = variableName;

            inputParameters.Add(this.variableName);
        }

        public override async Task Execute()
        {
            VariableDictionary.Get(variableName, out IVariable<double> variable);
            valueGot = variable.Value;

            outputParameters.Add(valueGot);
        }
    }
}