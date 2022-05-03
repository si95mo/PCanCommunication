using DataStructures.VariablesDictionary;
using Hardware.Can;
using System.Diagnostics;
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
        /// <param name="timeout">The timeout (in ms)</param>
        /// <param name="description">The description</param>
        public Set(string variableName, double valueToSet, int id, int order, int timeout = 1000, string description = "")
            : base("Set", id, order, timeout, description)
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
            Rx.CanFrameChanged += LocalRx_CanFrameChanged;

            startTime = System.DateTime.Now;
            outputParameters.Clear();

            await Task.Run(() =>
                {
                    VariableDictionary.Get(variableName, out IVariable variable);
                    variable.ValueAsObject = valueToSet;

                    // Update can channel value and send
                    Tx.Cmd = 1;
                    (variable as Variable<double>).UpdateIndexedCanChannel(Tx);
                }
            );

            Task<bool> waitTask = Task.Run(async () =>
                {
                    VariableDictionary.Get(variableName, out IVariable variable);
                    Tx.Cmd = 1;

                    Stopwatch time = Stopwatch.StartNew();

                    received = false;
                    (variable as Variable<double>).UpdateIndexedCanChannel(Tx);

                    // Wait for event fired or timeout occurred
                    while (!received && time.Elapsed.TotalMilliseconds <= timeout)
                        await Task.Delay(20);

                    time.Stop();

                    return received;
                }
            );

            result = await waitTask;
            Result = result;

            stopTime = System.DateTime.Now;

            Rx.CanFrameChanged -= LocalRx_CanFrameChanged;

            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }

        private void LocalRx_CanFrameChanged(object sender, CanFrameChangedEventArgs e)
        {
            received = true; // Event received
        }

        public override string ToString()
        {
            string description = $"{name}\t " +
                $"{id}\t " +
                $"{order}\t " +
                $"{variableName}\t " +
                $"{valueToSet}\t \t " +
                $"{startTime:HH:mm:ss.fff}\t " +
                $"{stopTime:HH:mm:ss.fff}\t " +
                $"{result}";
            return description;
        }
    }
}