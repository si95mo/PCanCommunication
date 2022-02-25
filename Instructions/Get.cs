using DataStructures.VariablesDictionary;
using Hardware.Can;
using System;
using System.Diagnostics;
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
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        /// <param name="timeout">The timeout (in ms)</param>
        /// <param name="description">The description</param>
        public Get(string variableName, int id, int order, int timeout = 1000, string description = "")
            : base("Get", id, order, timeout, description)
        {
            this.variableName = variableName;

            inputParameters.Add(this.variableName);
        }

        /// <summary>
        /// Execute the <see cref="Get"/> instruction
        /// </summary>
        public override async Task Execute()
        {
            Rx.CanFrameChanged += LocalRx_CanFrameChanged;

            startTime = DateTime.Now;
            outputParameters.Clear();

            await Task.Run(() =>
                {
                    // Get the variable
                    VariableDictionary.Get(variableName, out IVariable variable);

                    // Update the indexed can channel
                    Tx.Cmd = 0;
                    (variable as Variable<double>).UpdateIndexedCanChannel(Tx);

                    outputParameters.Add(valueGot);
                }
            );

            Task<bool> waitTask = Task.Run(async () =>
                {
                    VariableDictionary.Get(variableName, out IVariable variable);
                    Tx.Cmd = 0;

                    Stopwatch time = Stopwatch.StartNew();

                    received = false;
                    (variable as Variable<double>).UpdateIndexedCanChannel(Tx);

                    // Await the receive event or timeout occurred
                    while (!received && time.Elapsed.TotalMilliseconds <= timeout)
                        await Task.Delay(50);

                    time.Stop();

                    return received;
                }
            );

            result = await waitTask;
            Result = result;

            stopTime = DateTime.Now;

            Rx.CanFrameChanged -= LocalRx_CanFrameChanged;

            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }

        public override string ToString()
        {
            string description = $"{name}\t " +
                $"{id}\t " +
                $"{order}\t " +
                $"{variableName}\t " +
                $"{valueGot}\t \t " +
                $"{startTime:HH:mm:ss.fff}\t " +
                $"{stopTime:HH:mm:ss.fff}\t " +
                $"{result}";
            return description;
        }

        // IndexedCanChannel value changed event
        private void LocalRx_CanFrameChanged(object sender, CanFrameChangedEventArgs e)
        {
            VariableDictionary.Get(variableName, out IVariable variable);
            (variable as DoubleVariable).Value = variable.Type == VariableType.Sgl ?
                BitConverter.ToSingle((e.NewCanFrame as CanFrame).Data, 4) : BitConverter.ToInt32((e.NewCanFrame as CanFrame).Data, 4);

            // The value retrieved from the CAN buss
            valueGot = Convert.ToDouble(variable.ValueAsObject);

            // Event fired
            received = true;
        }
    }
}