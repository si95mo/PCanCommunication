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
        public Wait(int delay) : base("Wait")
        {
            this.delay = delay;

            inputParameters.Add(delay);
        }

        public override async void Execute() => await Task.Delay(delay);
    }
}