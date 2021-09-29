using System;
using System.Collections.Generic;
using System.IO;

namespace Instructions.Scheduler
{
    /// <summary>
    /// Load a test program from a csv file
    /// </summary>
    internal static class TestProgramManager
    {
        private static object lockObject = new object();

        /// <summary>
        /// Load a test program from disk
        /// </summary>
        /// <param name="path">The test program path</param>
        /// <param name="delimeter">The delimiter <see cref="char"/></param>
        /// <returns>A <see cref="List"/> with the retrieved <see cref="Instruction"/></returns>
        internal static List<Instruction> ReadTest(string path, char delimeter = ';')
        {
            List<Instruction> instructions = new List<Instruction>();

            string[] testProgram = File.ReadAllLines(path);
            string[][] testParsed = new string[testProgram.Length][];

            Instruction instruction;
            for (int i = 0; i < testProgram.Length; i++)
            {
                testParsed[i] = testProgram[i].Split(';');

                switch (testParsed[i][1].Trim())
                {
                    case "GET":
                        instruction = new Get(
                            testParsed[i][3].Trim(),           // Variable name
                            int.Parse(testParsed[i][0].Trim()),
                            int.Parse(testParsed[i][2].Trim()) // Order
                        );
                        break;

                    case "SET":
                        instruction = new Set(
                            testParsed[i][3].Trim(),               // Variable name
                            double.Parse(testParsed[i][4].Trim()), // Value
                            int.Parse(testParsed[i][0].Trim()),
                            int.Parse(testParsed[i][2].Trim())     // Order
                        );
                        break;

                    case "WAIT":
                        instruction = new Wait(
                            int.Parse(testParsed[i][3].Trim()), // Delay
                            int.Parse(testParsed[i][0].Trim()),
                            int.Parse(testParsed[i][2].Trim())  // Order
                        );
                        break;

                    case "WAIT_FOR":
                        ConditionOperand operand = ConditionOperand.Equal;
                        switch (testParsed[i][5].Trim())
                        {
                            case "==":
                                operand = ConditionOperand.Equal;
                                break;

                            case "!=":
                                operand = ConditionOperand.NotEqual;
                                break;

                            case "<":
                                operand = ConditionOperand.Lesser;
                                break;

                            case ">":
                                operand = ConditionOperand.Greather;
                                break;
                        }

                        instruction = new WaitFor(
                            testParsed[i][3].Trim(),            // First variable name
                            testParsed[i][4].Trim(),            // Second variable name
                            operand,                            // Operand
                            int.Parse(testParsed[i][6].Trim()), // Condition time
                            int.Parse(testParsed[i][7].Trim()), // Timeout
                            int.Parse(testParsed[i][0].Trim()),
                            int.Parse(testParsed[i][2].Trim())  // Order
                        );
                        break;

                    default:
                        instruction = null;
                        break; // ?
                }

                instructions.Add(instruction);
            }

            return instructions;
        }

        /// <summary>
        /// Append an <see cref="Instruction"/> result to disk
        /// </summary>
        /// <param name="path">The path in which save the result</param>
        /// <param name="instruction">The <see cref="Instruction"/> of which save the result</param>
        internal static void SaveResult(string path, Instruction instruction)
        {
            string result = "";

            if (instruction is Get)
                result += "GET; ";

            if (instruction is Set)
                result += "SET; ";

            if (instruction is Wait)
                result += "WAIT; ";

            if (instruction is WaitFor)
                result += "WAIT_FOR; ";

            result += $"id: {instruction.Id}; ";
            result += $"order: {instruction.Order}; ";

            foreach (object o in instruction.InputParameters)
                result += $"in: {o}; ";

            foreach (object o in instruction.OutputParameters)
            {
                if (o is DateTime)
                    result += $"out: {(DateTime)o:HH:mm:ss:fff}; ";
                else
                    result += $"out: {o}; ";
            }

            result.Trim();
            result.Remove(result.Length - 1);
            result += Environment.NewLine;

            lock (lockObject)
                File.AppendAllText(path, result);
        }
    }
}