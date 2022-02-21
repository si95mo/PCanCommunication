using DataStructures.VariablesDictionary;
using Hardware.Can;
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
        Lesser = 3,

        /// <summary>
        /// '<' operand
        /// </summary>
        Included = 4
    }

    /// <summary>
    /// Implement an <see cref="Instruction"/> that wait for a condition
    /// </summary>
    public class WaitFor : Instruction
    {
        private string variableName;
        private double value;
        protected double valueGot;
        private ConditionOperand operand;
        private int conditionTime;
        private int pollingInterval;

        /// <summary>
        /// Create a new instance of <see cref="WaitFor"/>
        /// </summary>
        /// <param name="variableName">The first variable in the condition name</param>
        /// <param name="value">The value to test</param>
        /// <param name="operand">The <see cref="ConditionOperand"/></param>
        /// <param name="conditionTime">The time (in milliseconds) in which the condition must remain <see langword="true"/></param>
        /// <param name="timeout">The timeout (in milliseconds)</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        /// <param name="pollingInterval">The polling interval (in milliseconds)</param>
        /// <param name="description">The description</param>
        public WaitFor(string variableName, double value, ConditionOperand operand,
            int conditionTime, int timeout, int id, int order, int pollingInterval = 200, string description = "")
            : base("WaitFor", id, order, description: description)
        {
            this.variableName = variableName;
            this.value = value;
            this.operand = operand;
            this.timeout = timeout;
            this.conditionTime = conditionTime;
            this.pollingInterval = pollingInterval;

            inputParameters.Add(this.variableName);
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
            Rx.CanFrameChanged += LocalRx_CanFrameChanged;

            startTime = DateTime.Now;
            outputParameters.Clear();

            bool condition()
            {
                bool returnValue = false;

                VariableDictionary.Get(variableName, out IVariable firstVariable);

                valueGot = Convert.ToDouble(firstVariable.ValueAsObject);
                double threshold = 0.000001;
                switch (operand)
                {
                    case ConditionOperand.Equal:
                        returnValue = Math.Abs(valueGot - value) <= threshold;
                        break;

                    case ConditionOperand.NotEqual:
                        returnValue = Math.Abs(valueGot - value) > threshold;
                        break;

                    case ConditionOperand.Greather:
                        returnValue = valueGot > value + threshold;
                        break;

                    case ConditionOperand.Lesser:
                        returnValue = valueGot < value - threshold;
                        break;

                    case ConditionOperand.Included:
                        returnValue = (valueGot < value - threshold) && (valueGot > value - threshold);
                        break;
                }

                return returnValue;
            }

            Task<bool> waitTask = Task.Run(async () =>
                {
                    bool cond = false;

                    VariableDictionary.Get(variableName, out IVariable variable);
                    Tx.Cmd = 0;

                    Stopwatch time = Stopwatch.StartNew();
                    while (!cond && time.Elapsed.TotalMilliseconds <= timeout)
                    {
                        received = false;
                        (variable as Variable<double>).UpdateVariable(Tx);

                        while (!received && time.Elapsed.TotalMilliseconds <= timeout)
                            await Task.Delay(pollingInterval);

                        cond = condition();

                        if (cond)
                            return true;
                    }

                    time.Stop();
                    return false;

                    //Stopwatch sw = Stopwatch.StartNew();
                    //while (sw.Elapsed.TotalMilliseconds < conditionTime != !cond)
                    //{
                    //    await getTask;
                    //    await Task.Delay(pollingInterval);

                    //    cond = condition();
                    //}
                }
            );

            result = await waitTask;
            stopTime = DateTime.Now;

            Rx.CanFrameChanged -= LocalRx_CanFrameChanged;

            outputParameters.Add(result);
            outputParameters.Add(valueGot);
            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }

        private void LocalRx_CanFrameChanged(object sender, CanFrameChangedEventArgs e)
        {
            VariableDictionary.Get(variableName, out IVariable variable);
            (variable as DoubleVariable).Value = variable.Type == VariableType.Sgl ?
                BitConverter.ToSingle((e.NewCanFrame as CanFrame).Data, 4) : BitConverter.ToInt32((e.NewCanFrame as CanFrame).Data, 4);

            valueGot = Convert.ToDouble(variable.ValueAsObject);

            received = true;
        }

        public override string ToString()
        {
            string description = $"{name}\t" +
                $"{id}\t" +
                $"{order}\t" +
                $"{variableName}\t \t " +
                $"{variableName} ({valueGot}) is {operand} than {value}\t " +
                $"{startTime:HH:mm:ss.fff}\t " +
                $"{stopTime:HH:mm:ss.fff}\t " +
                $"{result}";
            return description;
        }
    }
}