using Hardware.Can;
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
        protected int id;
        protected bool result;
        protected int timeout;
        protected string description;
        protected bool received;

        protected DateTime startTime;
        protected DateTime stopTime;

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
        /// The <see cref="Instruction"/> description
        /// </summary>
        public string Description
        {
            get => description;
            set => description = value;
        }

        public IndexedCanChannel Tx { get; set; }
        public IndexedCanChannel Rx { get; set; }
        public ICanResource Resource { get; set; }

        /// <summary>
        /// Initialize the <see cref="Instruction"/> attributes
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        /// <param name="timeout">The timeout (in ms)</param>
        /// <param name="description">The description</param>
        protected Instruction(string name, int id, int order, int timeout = 1000, string description = "")
        {
            this.name = name;
            this.id = id;
            this.order = order;
            this.timeout = timeout;
            this.description = description;

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