using System.Threading.Tasks;

namespace Instructions
{
    /// <summary>
    /// Implement an <see cref="Instruction"/> that wait for an amount of time
    /// </summary>
    public class Wait : Instruction
    {
        private int delay;

        /// <summary>
        /// Create a new instance of <see cref="Wait"/>
        /// </summary>
        /// <param name="delay">The delay (in milliseconds)</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        public Wait(int delay, int id, int order) : base("Wait", id, order)
        {
            this.delay = delay;

            inputParameters.Add(delay);
        }

        /// <summary>
        /// Execute the <see cref="Wait"/> instruction
        /// </summary>
        public override async Task Execute()
        {
            startTime = System.DateTime.Now;
            outputParameters.Clear();

            await Task.Delay(delay);

            stopTime = System.DateTime.Now;

            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }

        public override string ToString()
        {
            string description = $"{name}; " +
                $"{id}; " +
                $"{order}; " +
                $" ; ; ; " +
                $"{startTime:HH:mm:ss.fff}; " +
                $"{stopTime:HH:mm:ss.fff}; ";
            return description;
        }
    }
}