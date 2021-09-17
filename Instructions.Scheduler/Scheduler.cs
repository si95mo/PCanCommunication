using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Instructions.Scheduler
{
    /// <summary>
    /// Implement a simple <see cref="Instruction"/> scheduler
    /// that will execute all the subscribed elements sequentially
    /// </summary>
    public class Scheduler
    {
        private Queue<Instruction> instructions;

        /// <summary>
        /// The subscribed <see cref="Instruction"/>
        /// </summary>
        public Queue<Instruction> Instructions => instructions;

        /// <summary>
        /// Create a new instance of <see cref="Scheduler"/>
        /// </summary>
        /// <param name="path">The test program file path</param>
        public Scheduler(string path)
        {
            instructions = new Queue<Instruction>();
            TestProgramHandler.ReadTest(path).ForEach(x => Add(x));
        }

        /// <summary>
        /// Add an <see cref="Instruction"/> to the
        /// <see cref="Instructions"/>
        /// </summary>
        /// <param name="instruction">The <see cref="Instruction"/> to add</param>
        public void Add(Instruction instruction)
            => instructions.Enqueue(instruction);

        /// <summary>
        /// Execute all the subscribed <see cref="Instruction"/>
        /// and remove them from <see cref="Instructions"/>
        /// </summary>
        public async Task ExecuteAll()
        {
            while (instructions.Count > 0)
            {
                Instruction instruction = instructions.Dequeue();
                await instruction.Execute();

                string path = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "result.csv"
                );
                TestProgramHandler.SaveResult(path, instruction);
            }
        }
    }
}