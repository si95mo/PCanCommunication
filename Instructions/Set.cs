using DataStructures.VariablesDictionary;
using Hardware.Can;
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
        public Set(string variableName, double valueToSet, int id, int order, int timeout = 1000,
            IndexedCanChannel rx = null, IndexedCanChannel tx = null) : base("Set", id, order, timeout, rx, tx)
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

            if (tx != null)
                tx.Cmd = 1;

            // Timeout handling
            Task t = await Task.WhenAny(waitTask, Task.Delay(timeout));
            result = waitTask == t;

            // Set instruction
            if (result)
            {
                await Task.Run(() =>
                    {
                        VariableDictionary.Get(variableName, out IVariable variable);
                        variable.ValueAsObject = valueToSet;
                    }
                );
            }

            stopTime = System.DateTime.Now;

            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }

        public override string ToString()
        {
            string description = $"{name}\t" +
                $"{id}\t" +
                $"{order}\t" +
                $"{variableName}\t" +
                $"{valueToSet}\t\t" +
                $"{startTime:HH:mm:ss.fff}\t" +
                $"{stopTime:HH:mm:ss.fff}\t" +
                $"{result}";
            return description;
        }
    }
}