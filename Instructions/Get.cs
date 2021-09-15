using DataStructure.VariablesDictionary;

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
        public Get(string variableName) : base("Get")
        {
            this.variableName = variableName;

            inputParameters.Add(this.variableName);
            outputParameters.Add(valueGot);
        }

        public override void Execute()
        {
            VariableDictionary.Get(variableName, out IVariable<double> variable);
            valueGot = variable.Value;
        }
    }
}