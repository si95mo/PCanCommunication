using System;
using System.Collections.Generic;
using System.IO;

namespace Instructions.Scheduler
{
    /// <summary>
    /// Load a test program from a csv file
    /// </summary>
    internal static class TestProgramHandler
    {
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

                switch (testParsed[i][0])
                {
                    case "GET":
                        instruction = new Get(testParsed[i][1].Trim(), 0);
                        break;

                    case "SET":
                        instruction = new Set(
                            testParsed[i][1].Trim(),
                            double.Parse(testParsed[i][2].Trim()),
                            0
                        );
                        break;

                    case "WAIT":
                        instruction = new Wait(
                            int.Parse(testParsed[i][1].Trim()),
                            int.Parse(testParsed[i][2].Trim())
                        );
                        break;

                    case "WAIT_FOR":
                        ConditionOperand operand = ConditionOperand.Equal;
                        switch (testParsed[i][3].Trim())
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
                            testParsed[i][1].Trim(),
                            testParsed[i][2].Trim(),
                            operand,
                            int.Parse(testParsed[i][4].Trim()),
                            0
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

            foreach (object o in instruction.InputParameters)
                result += $"in: {o}; ";

            foreach (object o in instruction.OutputParameters)
                result += $"out: {o}; ";

            result.Trim();
            result.Remove(result.Length - 1);
            result += Environment.NewLine;

            File.AppendAllText(path, result);
        }
    }
}