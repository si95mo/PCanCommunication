using Hardware.Can;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        internal static List<Instruction> ReadTest(string path, IndexedCanChannel rx, IndexedCanChannel tx,char delimiter = ';')
        {
            List<Instruction> instructions = new List<Instruction>();

            string[] testProgram = File.ReadAllLines(path);
            string[][] testParsed = new string[testProgram.Length][];

            Instruction instruction;
            for (int i = 1; i < testProgram.Length; i++) // No headers
            {
                testParsed[i] = testProgram[i].Split(delimiter);

                int.TryParse(testParsed[i][0].Trim(), out int id);
                int.TryParse(testParsed[i][1].Trim(), out int order);
                string variableName = testParsed[i][3].Trim();
                double.TryParse(
                    testParsed[i][4].TrimEnd(),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out double value
                );
                string condition = testParsed[i][5].Trim();
                double.TryParse(
                    testParsed[i][6].Trim(),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out double highLim
                );
                double.TryParse(
                    testParsed[i][7].Trim(),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out double lowLim
                );
                int.TryParse(testParsed[i][8].Trim(), out int time);
                int.TryParse(testParsed[i][9].Trim(), out int timeout);

                bool skip = testParsed[i][10].Trim().CompareTo("") != 0; // "" then do not skip, anything else then skip the instruction

                if (!skip)
                {
                    string instructionType = testParsed[i][2].Trim();
                    switch (instructionType)
                    {
                        case "GET":
                            instruction = new Get(variableName, id, order, timeout, rx, tx);
                            break;

                        case "SET":
                            instruction = new Set(variableName, value, id, order, timeout, rx, tx);
                            break;

                        case "WAIT":
                            instruction = new Wait(time, id, order);
                            break;

                        case "TEST":
                            instruction = new Test(variableName, id, order, value, ParseOperand(condition), timeout, rx, tx);
                            break;

                        case "WAIT_FOR":
                            instruction = new WaitFor(
                                variableName,
                                value,
                                ParseOperand(condition),
                                time,
                                timeout,
                                id,
                                order,
                                pollingInterval: 50,
                                rx,
                                tx
                            );
                            break;

                        default:
                            instruction = null;
                            break; // ?
                    }

                    instructions.Add(instruction);
                }
            }

            return instructions;
        }

        private static ConditionOperand ParseOperand(string condition)
        {
            ConditionOperand operand = ConditionOperand.Equal;
            switch (condition)
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

            return operand;
        }

        /// <summary>
        /// Append an <see cref="Instruction"/> result to disk
        /// </summary>
        /// <param name="path">The path in which save the result</param>
        /// <param name="instruction">The <see cref="Instruction"/> of which save the result</param>
        internal static void SaveResult(string path, Instruction instruction)
        {
            lock (lockObject)
            {
                InitializeFile(path);
                File.AppendAllText(path, $"{instruction}{Environment.NewLine}");
            }
        }

        /// <summary>
        /// Initialize the file by writing the headers
        /// </summary>
        /// <param name="path">The file path</param>
        private static void InitializeFile(string path)
        {
            if (!File.Exists(path))
                File.AppendAllText(
                    path,
                    $"Name; ID; Order; Variable involved; Value; Condition to verify; Start time; Stop time; Result{Environment.NewLine}"
                );
        }
    }
}