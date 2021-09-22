using DataStructures.VariablesDictionary;
using System;
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

        /// <summary>
        /// Execute the <see cref="Get"/> instruction
        /// </summary>
        public override async Task Execute()
        {
            startTime = DateTime.Now;
            outputParameters.Clear();

            await Task.Run(() =>
                {
                    VariableDictionary.Get(variableName, out IVariable variable);
                    valueGot = Convert.ToDouble(variable.ValueAsObject);

                    outputParameters.Add(valueGot);
                }
            );

            stopTime = DateTime.Now;

            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }
    }
}