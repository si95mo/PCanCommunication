using Dasync.Collections;
using Hardware.Can;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instructions.Scheduler
{
    /// <summary>
    /// Handles the property instruction log event.
    /// See also <see cref="EventArgs"/>
    /// </summary>
    public class InstructionLogChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The old value
        /// </summary>changed
        public readonly string OldValue;

        /// <summary>
        /// The new value
        /// </summary>
        public readonly string NewValue;

        /// <summary>
        /// Create a new instance of <see cref="InstructionLogChangedEventArgs"/>
        /// </summary>
        /// <param name="oldValue">The old value</param>
        /// <param name="newValue">The new value</param>
        public InstructionLogChangedEventArgs(string oldValue, string newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    /// <summary>
    /// Implement a simple <see cref="Instruction"/> scheduler
    /// that will execute all the subscribed elements sequentially
    /// </summary>
    public class Scheduler
    {
        private SortedDictionary<int, Queue<Instruction>> instructions;
        private bool stop;
        private object objectLock = new object();
        private string instructionLog = "";

        public string ActualInstructionDescription { get; private set; } = "";
        public string InstructionLog 
        {
            get => instructionLog; 
            private set
            {
                if(instructionLog.CompareTo(value) != 0)
                {
                    string lastLog = instructionLog;
                    instructionLog = value;
                    OnStatusChanged(new InstructionLogChangedEventArgs(lastLog, instructionLog));
                }
            }
        }

        public bool TestResult { get; private set; } = false;

        /// <summary>
        /// The subscribed <see cref="Instruction"/>
        /// </summary>
        public Queue<Instruction> Instructions
        {
            get
            {
                Queue<Instruction> instructionQueue = new Queue<Instruction>();

                foreach (Queue<Instruction> instruction in instructions.Values)
                    instruction.ToList().ForEach(x => instructionQueue.Enqueue(x));

                return instructionQueue;
            }
        }

        /// <summary>
        /// The <see cref="StatusChanged"/> handler
        /// </summary>
        protected EventHandler<InstructionLogChangedEventArgs> InstructionLogChangedHandler;

        /// <summary>
        /// The <see cref="InstructionLogChangedEventArgs"/> event handler
        /// for the <see cref="Status"/> property
        /// </summary>
        public event EventHandler<InstructionLogChangedEventArgs> InstructionLogChanged
        {
            add
            {
                lock (objectLock)
                    InstructionLogChangedHandler += value;
            }
            remove
            {
                lock (objectLock)
                    InstructionLogChangedHandler -= value;
            }
        }

        /// <summary>
        /// On status changed event
        /// </summary>
        /// <param name="e">The <see cref="InstructionLogChanged"/></param>
        protected virtual void OnStatusChanged(InstructionLogChangedEventArgs e)
            => InstructionLogChangedHandler?.Invoke(this, e);

        /// <summary>
        /// Create a new instance of <see cref="Scheduler"/>
        /// </summary>
        /// <param name="path">The test program file path</param>
        public Scheduler(string path)
        {
            instructions = new SortedDictionary<int, Queue<Instruction>>();

            TestProgramManager.ReadMain(path, pathString: "->", delimiter: '\t').ForEach(x => Add(x));

            stop = false;

            instructionLog = "";
        }

        /// <summary>
        /// Add an <see cref="Instruction"/> to the
        /// <see cref="Instructions"/>
        /// </summary>
        /// <param name="instruction">The <see cref="Instruction"/> to add</param>
        public void Add(Instruction instruction)
        {
            if (instructions.ContainsKey(instruction.Order))
                instructions[instruction.Order].Enqueue(instruction);
            else
            {
                instructions.Add(instruction.Order, new Queue<Instruction>());
                instructions[instruction.Order].Enqueue(instruction);
            }
        }

        /// <summary>
        /// Execute all the subscribed <see cref="Instruction"/>
        /// and remove them from <see cref="Instructions"/>
        /// </summary>
        /// <param name="path">The result path (if not specified,
        /// file will be saved in Desktop)</param>
        public async Task ExecuteAll(string path = "", ICanResource resource = null, IndexedCanChannel tx = null, IndexedCanChannel rx = null)
        {
            stop = false;
            bool instructionResult = true;

            if (path.CompareTo("") == 0)
                path = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "result.csv"
                );

            int order = instructions.Keys.Min();
            List<Instruction> instructionList = new List<Instruction>();

            while (instructions.Count > 0 && !stop)
            {
                while (instructions[order].Count > 0)
                {
                    Instruction instruction = instructions[order].Dequeue();
                    instructionList.Add(instruction);
                }

                await instructionList.ParallelForEachAsync(
                    async (x) =>
                    {
                        if (instructionResult)
                        {
                            // Can channels link
                            x.Tx = tx;
                            x.Rx = rx;

                            // Can resource link
                            x.Resource = resource;
                            
                            // Execute instruction
                            await x.Execute();
                        }

                        instructionResult &= x.Result;

                        ActualInstructionDescription = x.Description;
                        InstructionLog = x.ToString();

                        TestProgramManager.SaveResult(path, x);
                    }
                );

                instructions.Remove(order);
                order = instructions.Count > 0 ? instructions.Keys.Min() : 0;
                instructionList.Clear();

                TestResult = instructionResult;
            }
        }

        /// <summary>
        /// Stop the current execution
        /// </summary>
        public void Stop() => stop = true;
    }
}