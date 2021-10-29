using DataStructures.VariablesDictionary;
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

        protected bool result;

        /// <summary>
        /// Create a new instance of <see cref="Test"/>
        /// </summary>
        /// <param name="variableName">The variable name</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        public Test(string variableName, int id, int order, double value, ConditionOperand operand) : base("Get", id, order)
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

            await Task.Run(() =>
                {
                    VariableDictionary.Get(variableName, out IVariable variable);
                    valueGot = Convert.ToDouble(variable.ValueAsObject);

                    outputParameters.Add(valueGot);
                }
            );

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

            stopTime = DateTime.Now;

            outputParameters.Add(result);
            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }
    }
}