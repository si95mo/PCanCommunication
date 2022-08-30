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
        internal SortedDictionary<int, Queue<Instruction>> EndingSequence { get; set; }

        private SortedDictionary<int, Queue<Instruction>> instructions;
        private bool stop;
        private object objectLock = new object();
        private string instructionLog = "";

        public string ActualInstructionDescription { get; private set; } = "";

        /// <summary>
        /// The <see cref="Scheduler"/> <see cref="Instruction"/> log
        /// (i.e. last <see cref="Instruction"/> executed)
        /// </summary>
        public string InstructionLog
        {
            get => instructionLog;
            private set
            {
                if (instructionLog.CompareTo(value) != 0)
                {
                    string lastLog = instructionLog;
                    instructionLog = value;
                    OnStatusChanged(new InstructionLogChangedEventArgs(lastLog, instructionLog));
                }
            }
        }

        public string FullLog { get; private set; } = "";

        /// <summary>
        /// The test execution result
        /// </summary>
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
        /// <param name="testProgramPath">The test program file path</param>
        /// <param name="batchFilePath">The batch file path</param>
        public Scheduler(string testProgramPath, string batchFilePath)
        {
            EndingSequence = new SortedDictionary<int, Queue<Instruction>>();

            instructions = new SortedDictionary<int, Queue<Instruction>>();
            stop = false;
            instructionLog = "";

            InstructionLogChanged += Scheduler_InstructionLogChanged;

            List<Instruction> testProgram = TestProgramManager.ReadMain(testProgramPath, batchFilePath, pathString: "->", delimiter: '\t');
            testProgram.ForEach(x => Add(x)); // Normal test program
            int n = TestProgramManager.ReadTest(TestProgramManager.EndingSequencePath, batchFilePath, delimiter: '\t').Count; // Ending sequence
            for (int i = testProgram.Count - n; i < testProgram.Count; i++)
                AddToEndingSequence(testProgram[i]);
        }

        private void Scheduler_InstructionLogChanged(object sender, InstructionLogChangedEventArgs e)
            => FullLog += InstructionLog;

        /// <summary>
        /// Add an <see cref="Instruction"/> to the <see cref="Instructions"/>
        /// </summary>
        /// <param name="instruction">The <see cref="Instruction"/> to add</param>
        public void Add(Instruction instruction)
        {
            // Add an instruction with regard to its order
            if (instructions.ContainsKey(instruction.Order))
                instructions[instruction.Order].Enqueue(instruction);
            else
            {
                instructions.Add(instruction.Order, new Queue<Instruction>());
                instructions[instruction.Order].Enqueue(instruction);
            }
        }

        /// <summary>
        /// Add an <see cref="Instruction"/> to the <see cref="EndingSequence"/>
        /// </summary>
        /// <param name="instruction">The <see cref="Instruction"/> to add</param>
        internal void AddToEndingSequence(Instruction instruction)
        {
            // Add an instruction with regard to its order
            if (EndingSequence.ContainsKey(instruction.Order))
                EndingSequence[instruction.Order].Enqueue(instruction);
            else
            {
                EndingSequence.Add(instruction.Order, new Queue<Instruction>());
                EndingSequence[instruction.Order].Enqueue(instruction);
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
            FullLog = "";

            stop = false;

            // Result path, if not previously valorized
            if (path.CompareTo("") == 0)
                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "result.csv");

            // Normal execution
            bool instructionResut = await Execute(instructions, resource, tx, rx, path);

            // If an error occurred, run the ending sequence of instruction
            if (!instructionResut)
                await Execute(EndingSequence, resource, tx, rx, path);

            // Add the final lines to the result file
            TestProgramManager.FinalizeFile(path, TestResult);
        }

        /// <summary>
        /// Execute a list of <see cref="Instruction"/>
        /// </summary>
        /// <param name="list">The <see cref="Instruction"/> collection</param>
        /// <param name="resource">The <see cref="ICanResource"/></param>
        /// <param name="tx">The TX <see cref="IndexedCanChannel"/></param>
        /// <param name="rx">The RX <see cref="IndexedCanChannel"/></param>
        /// <returns><see langword="true"/> if the execution succeeded, <see langword="false"/> otherwise</returns>
        private async Task<bool> Execute(SortedDictionary<int, Queue<Instruction>> list, ICanResource resource,
            IndexedCanChannel tx, IndexedCanChannel rx, string path)
        {
            bool instructionResult = true;
            int order = list.Keys.Min();
            List<Instruction> instructionList = new List<Instruction>();

            while (list.Count > 0 && !stop && instructionResult)
            {
                // Retrieve all the instructions with the same order
                while (list[order].Count > 0)
                {
                    Instruction instruction = list[order].Dequeue();
                    instructionList.Add(instruction);
                }

                // And execute them asynchronously in parallel
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

                        // Get the result
                        instructionResult &= x.Result;

                        // And then eventually fire the events
                        ActualInstructionDescription = x.Description;
                        InstructionLog = x.ToString();

                        // Save the instruction result
                        TestProgramManager.SaveResult(path, x);
                    }
                );

                list.Remove(order);
                order = list.Count > 0 ? list.Keys.Min() : 0;
                instructionList.Clear();

                TestResult = instructionResult;
            }

            return instructionResult;
        }

        /// <summary>
        /// Stop the current execution
        /// </summary>
        public void Stop() => stop = true;
    }
}