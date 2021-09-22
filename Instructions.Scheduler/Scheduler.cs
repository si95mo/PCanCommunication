using Dasync.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instructions.Scheduler
{
    /// <summary>
    /// Implement a simple <see cref="Instruction"/> scheduler
    /// that will execute all the subscribed elements sequentially
    /// </summary>
    public class Scheduler
    {
        private SortedDictionary<int, Queue<Instruction>> instructions;

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
        /// Create a new instance of <see cref="Scheduler"/>
        /// </summary>
        /// <param name="path">The test program file path</param>
        public Scheduler(string path)
        {
            instructions = new SortedDictionary<int, Queue<Instruction>>();
            TestProgramManager.ReadTest(path).ForEach(x => Add(x));
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
        public async Task ExecuteAll()
        {
            string path = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "result.csv"
            );
            int order = instructions.Keys.Min();
            List<Instruction> instructionList = new List<Instruction>();

            while (instructions.Count > 0)
            {
                while (instructions[order].Count > 0)
                    instructionList.Add(instructions[order].Dequeue());

                await instructionList.ParallelForEachAsync(
                    async (x) =>
                    {
                        await x.Execute();
                        TestProgramManager.SaveResult(path, x);
                    }
                );

                instructions.Remove(order);
                order = instructions.Count > 0 ? instructions.Keys.Min() : 0;
                instructionList.Clear();
            }
        }
    }
}