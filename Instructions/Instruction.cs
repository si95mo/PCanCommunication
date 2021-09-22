using System;
using System.Collections.Generic;
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

        protected DateTime startTime;
        protected DateTime stopTime;

        /// <summary>
        /// The <see cref="Instruction"/> order index
        /// </summary>
        public int Order => order;

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
        /// Initialize the <see cref="Instruction"/> attributes
        /// </summary>
        /// <param name="name">The name</param>
        protected Instruction(string name, int order)
        {
            this.name = name;
            this.order = order;

            inputParameters = new List<object>();
            outputParameters = new List<object>();
        }

        /// <summary>
        /// Execute the <see cref="Instruction"/>
        /// </summary>
        /// <returns></returns>
        public abstract Task Execute();
    }
}