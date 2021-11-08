using DataStructures.VariablesDictionary;
using Hardware.Can;
using System;
using System.Threading.Tasks;

namespace Instructions
{
    public class Test : Instruction
    {
        protected string variableName;
        protected double valueGot;
        protected double value;
        protected ConditionOperand operand;

        /// <summary>
        /// Create a new instance of <see cref="Test"/>
        /// </summary>
        /// <param name="variableName">The variable name</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        public Test(string variableName, int id, int order, double value, ConditionOperand operand,
            int timeout = 1000, IndexedCanChannel rx = null, IndexedCanChannel tx = null) : base("Test", id, order, timeout, rx, tx)
        {
            this.variableName = variableName;
            this.value = value;
            this.operand = operand;

            inputParameters.Add(this.variableName);
            inputParameters.Add(this.value);
        }

        /// <summary>
        /// Execute the <see cref="Get"/> instruction
        /// </summary>
        public override async Task Execute()
        {
            startTime = DateTime.Now;
            outputParameters.Clear();

            // Timeout handling
            Task t = await Task.WhenAny(waitTask, Task.Delay(timeout));
            result = waitTask == t;

            // Test instruction
            if (result)
            {
                await Task.Run(() =>
                    {
                        VariableDictionary.Get(variableName, out IVariable variable);
                        valueGot = Convert.ToDouble(variable.ValueAsObject);

                        outputParameters.Add(valueGot);
                    }
                );

                TestValueGot();
            }

            stopTime = DateTime.Now;

            outputParameters.Add(result);
            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }

        private void TestValueGot()
        {
            double threshold = 0.000001;
            switch (operand)
            {
                case ConditionOperand.Equal:
                    result = Math.Abs(valueGot - value) <= threshold;
                    break;

                case ConditionOperand.NotEqual:
                    result = Math.Abs(valueGot - value) > threshold;
                    break;

                case ConditionOperand.Greather:
                    result = valueGot > value + threshold;
                    break;

                case ConditionOperand.Lesser:
                    result = valueGot < value - threshold;
                    break;
            }
        }

        public override string ToString()
        {
            string description = $"{name}\t" +
                $"{id}\t" +
                $"{order}\t" +
                $"{variableName}\t\t" +
                $"{variableName} ({valueGot}) is {operand} than {value}\t" +
                $"{startTime:HH:mm:ss.fff}\t" +
                $"{stopTime:HH:mm:ss.fff}\t" +
                $"{result}";
            return description;
        }
    }
}