using DataStructures.VariablesDictionary;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Instructions
{
    /// <summary>
    /// Define the type of operand to use in a condition
    /// </summary>
    public enum ConditionOperand
    {
        /// <summary>
        /// '==' operand
        /// </summary>
        Equal = 0,

        /// <summary>
        /// '!=' operand
        /// </summary>
        NotEqual = 1,

        /// <summary>
        /// '>' operand
        /// </summary>
        Greather = 2,

        /// <summary>
        /// '<' operand
        /// </summary>
        Lesser = 3
    }

    /// <summary>
    /// Implement an <see cref="Instruction"/> that wait for a condition
    /// </summary>
    public class WaitFor : Instruction
    {
        private string firstVariableName;
        private double value;
        private ConditionOperand operand;
        private int timeout;
        private int conditionTime;
        private int pollingInterval;

        private bool result;

        /// <summary>
        /// Create a new instance of <see cref="WaitFor"/>
        /// </summary>
        /// <param name="firstVariableName">The first variable in the condition name</param>
        /// <param name="value">The value to test</param>
        /// <param name="operand">The <see cref="ConditionOperand"/></param>
        /// <param name="conditionTime">The time (in milliseconds) in which the condition must remain <see langword="true"/></param>
        /// <param name="timeout">The timeout (in milliseconds)</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        /// <param name="pollingInterval">The polling interval (in milliseconds)</param>
        public WaitFor(string firstVariableName, double value, ConditionOperand operand,
            int conditionTime, int timeout, int id, int order, int pollingInterval = 10) : base("WaitFor", id, order)
        {
            this.firstVariableName = firstVariableName;
            this.value = value;
            this.operand = operand;
            this.timeout = timeout;
            this.conditionTime = conditionTime;
            this.pollingInterval = pollingInterval;

            inputParameters.Add(this.firstVariableName);
            inputParameters.Add(this.value);
            inputParameters.Add(this.operand);
            inputParameters.Add(this.conditionTime);
            inputParameters.Add(timeout);
        }

        /// <summary>
        /// Execute the <see cref="WaitFor"/> instruction
        /// </summary>
        public override async Task Execute()
        {
            startTime = DateTime.Now;
            outputParameters.Clear();

            bool condition()
            {
                bool returnValue = false;

                VariableDictionary.Get(firstVariableName, out IVariable firstVariable);

                double firstValue = Convert.ToDouble(firstVariable.ValueAsObject);
                double threshold = 0.000001;
                switch (operand)
                {
                    case ConditionOperand.Equal:
                        returnValue = Math.Abs(firstValue - value) <= threshold;
                        break;

                    case ConditionOperand.NotEqual:
                        returnValue = Math.Abs(firstValue - value) > threshold;
                        break;

                    case ConditionOperand.Greather:
                        returnValue = firstValue > value + threshold;
                        break;

                    case ConditionOperand.Lesser:
                        returnValue = firstValue < value - threshold;
                        break;
                }

                return returnValue;
            }

            Task waitTask = Task.Run(async () =>
                {
                    while (!condition())
                        await Task.Delay(pollingInterval);

                    Stopwatch sw = Stopwatch.StartNew();
                    while (sw.Elapsed.TotalMilliseconds < conditionTime != !condition())
                        await Task.Delay(pollingInterval);
                }
            );

            if (waitTask != await Task.WhenAny(waitTask, Task.Delay(timeout)))
                result = false;
            else
                result = true;

            stopTime = DateTime.Now;

            outputParameters.Add(result);

            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }
    }
}