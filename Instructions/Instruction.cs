using Hardware.Can;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Instructions
{
    /// <summary>
    /// Define a basic instruction
    /// </summary>
    public abstract class Instruction
    {
        protected string name;
        protected List<object> inputParameters;
        protected List<object> outputParameters;
        protected int order;
        protected int id;
        protected bool result;
        protected int timeout;

        protected DateTime startTime;
        protected DateTime stopTime;

        protected IndexedCanChannel rx;
        protected IndexedCanChannel tx;

        protected bool received;

        protected Task waitTask;

        /// <summary>
        /// The <see cref="Instruction"/> order index
        /// </summary>
        public int Order => order;

        /// <summary>
        /// The <see cref="Instruction"/> id
        /// </summary>
        public int Id => id;

        /// <summary>
        /// The <see cref="Instruction"/> input parameters
        /// </summary>
        public List<object> InputParameters => inputParameters;

        /// <summary>
        /// The <see cref="Instruction"/> output parameters
        /// </summary>
        public List<object> OutputParameters => outputParameters;

        /// <summary>
        /// The instruction name
        /// </summary>
        public string Name => name;

        /// <summary>
        /// The <see cref="Instruction"/> start time
        /// </summary>
        public DateTime StartTime => startTime;

        /// <summary>
        /// The <see cref="Instruction"/> stop time
        /// </summary>
        public DateTime StopTime => StopTime;

        /// <summary>
        /// The <see cref="Instruction"/> execution result
        /// </summary>
        public bool Result
        {
            get => result;
            set => result = value;
        }

        /// <summary>
        /// The <see cref="Instruction"/> timeout
        /// </summary>
        public int Timeout => timeout;

        /// <summary>
        /// Initialize the <see cref="Instruction"/> attributes
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        /// <param name="timeout">The timeout (in milliseconds)</param>
        /// <param name="rx">The RX <see cref="IndexedCanChannel"/></param>
        /// <param name="tx">The TX <see cref="IndexedCanChannel"/></param>
        protected Instruction(string name, int id, int order, int timeout = 1000, IndexedCanChannel rx = null, IndexedCanChannel tx = null)
        {
            this.name = name;
            this.id = id;
            this.order = order;
            this.timeout = timeout;

            this.rx = rx;
            this.tx = tx;

            received = false;

            inputParameters = new List<object>();
            outputParameters = new List<object>();

            // The waiting task
            waitTask = Task.Run(async () =>
                {
                    // Spin wait until condition is met (something has been received from the CAN bus)
                    while (!Condition())
                        await Task.Delay(10);

                    // Spin wait until timeout occurred
                    Stopwatch sw = Stopwatch.StartNew();
                    while (sw.Elapsed.TotalMilliseconds < timeout == !Condition())
                        await Task.Delay(10);
                }
             );

            if (rx != null)
                rx.CanFrameChanged += Rx_CanFrameChanged;
        }

        /// <summary>
        /// Execute the <see cref="Instruction"/>
        /// </summary>
        /// <returns></returns>
        public abstract Task Execute();

        /// <summary>
        /// Something has been received from the CAN bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rx_CanFrameChanged(object sender, CanFrameChangedEventArgs e)
            => received = true;

        /// <summary>
        /// True if somathing has been received from the CAN bus
        /// </summary>
        /// <returns></returns>
        protected bool Condition() => received;
    }
}