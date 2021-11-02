using DataStructures.VariablesDictionary;
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
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        public Set(string variableName, double valueToSet, int id, int order) : base("Set", id, order)
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
            startTime = System.DateTime.Now;
            outputParameters.Clear();

            await Task.Run(() =>
                {
                    VariableDictionary.Get(variableName, out IVariable variable);
                    variable.ValueAsObject = valueToSet;
                }
            );

            stopTime = System.DateTime.Now;

            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }

        public override string ToString()
        {
            string description = $"Instruction name: {name}; " +
                $"Instruction id: {id}; " +
                $"Instruction order: {order}; " +
                $"Involved variable: {variableName}; " +
                $"Value to set: {valueToSet}; " +
                $"Instruction start time: {startTime:HH:mm:ss.fff}; " +
                $"Instruction stop time: {stopTime:HH:mm:ss.fff}";
            return description;
        }
    }
}