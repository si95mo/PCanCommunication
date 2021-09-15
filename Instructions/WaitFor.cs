using DataStructure.VariablesDictionary;
using System;
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
        private string secondVariableName;
        private ConditionOperand operand;
        private int timeout;

        private bool result;

        /// <summary>
        /// Create a new instance of <see cref="WaitFor"/>
        /// </summary>
        /// <param name="firstVariableName">The first variable in the condition name</param>
        /// <param name="secondVariableName">The second variable in the condition name</param>
        /// <param name="operand">The <see cref="ConditionOperand"/></param>
        /// <param name="timeout">The timeout (in milliseconds)</param>
        /// <param name="order">The order index</param>
        public WaitFor(string firstVariableName, string secondVariableName, ConditionOperand operand, int timeout, int order) : base("WaitFor", order)
        {
            this.firstVariableName = firstVariableName;
            this.secondVariableName = secondVariableName;
            this.operand = operand;
            this.timeout = timeout;

            inputParameters.Add(this.firstVariableName);
            inputParameters.Add(this.secondVariableName);
            inputParameters.Add(this.operand);
            inputParameters.Add(timeout);

            outputParameters.Add(result);
        }

        public override async void Execute()
        {
            Func<bool> condition = () =>
            {
                bool returnValue = false;

                VariableDictionary.Get(firstVariableName, out IVariable<double> firstVariable);
                VariableDictionary.Get(secondVariableName, out IVariable<double> secondVariable);

                switch (operand)
                {
                    case ConditionOperand.Equal:
                        returnValue = firstVariable.Value == secondVariable.Value;
                        break;

                    case ConditionOperand.NotEqual:
                        returnValue = firstVariable.Value != secondVariable.Value;
                        break;

                    case ConditionOperand.Greather:
                        returnValue = firstVariable.Value > secondVariable.Value;
                        break;

                    case ConditionOperand.Lesser:
                        returnValue = firstVariable.Value < secondVariable.Value;
                        break;
                }

                return returnValue;
            };

            Task waitTask = Task.Run(async () =>
                {
                    while (condition()) 
                        await Task.Delay(10);
                }
            );

            if (waitTask != await Task.WhenAny(waitTask, Task.Delay(timeout)))
                result = false;
            else
                result = true;
        }
    }
}