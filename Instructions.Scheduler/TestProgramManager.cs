using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Instructions.Scheduler
{
    /// <summary>
    /// Load a test program from a csv file
    /// </summary>
    public static class TestProgramManager
    {
        public static int TxCanId { get; internal set; } = 0x200;
        public static int RxCanId { get; internal set; } = 0x100;

        public static string UserName { get; set; } = "";
        public static string ProductionSite { get; set; } = "";
        public static string BatchNumber { get; set; } = "";
        public static int SerialIndex { get; set; } = 0;
        public static string SerialNumber { get; set; } = "";

        private static object lockObject = new object();

        /// <summary>
        /// Read the main file and parse each sub-test file contained
        /// </summary>
        /// <param name="path">The main file path</param>
        /// <param name="pathString">The delimiter of the sub-test file path inside the main</param>
        /// <param name="delimiter">The sub-file char delimiter (<see cref="ReadTest(string, char)"/>)</param>
        /// <returns>The <see cref="List{T}"/> with all the parsed <see cref="Instruction"/></returns>
        internal static List<Instruction> ReadMain(string path, string pathString = "->", char delimiter = '\t')
        {
            List<Instruction> instructions = new List<Instruction>();

            // Read the main file
            string[] mainText = File.ReadAllLines(path);
            string basePath = Path.GetDirectoryName(path);
            string fullFileAbsolutePath = Path.Combine(basePath, "test.csv");

            // Delete previous created file, if exists
            if (File.Exists(fullFileAbsolutePath))
                File.Delete(fullFileAbsolutePath);

            int n = 0;
            for (int i = 0; i < mainText.Length; i++)
            {
                // If the line contains a sub-test file path
                if (mainText[i].StartsWith(pathString))
                {
                    // Parse it
                    string fileRelativePath = mainText[i].Replace(pathString, "").Trim();
                    string fileAbsolutePath = Path.Combine(basePath, fileRelativePath);

                    // Save the sub-test in the full-test file
                    n = SaveTest(fileAbsolutePath, ++n);
                }
                else
                {
                    if (mainText[i].StartsWith("@TX="))
                        TxCanId = Convert.ToInt32(mainText[i].Split('=')[1], 16);
                    else
                    {
                        if (mainText[i].StartsWith("@RX="))
                            RxCanId = Convert.ToInt32(mainText[i].Split('=')[1], 16);
                    }
                }
            }

            instructions.AddRange(ReadTest(fullFileAbsolutePath, delimiter));

            return instructions;
        }

        internal static int SaveTest(string subTestPath, int counter, string fullTestFileName = "test.csv", char delimiter = '\t')
        {
            int n;
            string basePath = Path.GetDirectoryName(subTestPath);
            string fullTestAbsolutePath = Path.Combine(basePath, fullTestFileName);

            // If the file does not exists, create it and write the headers
            if (!File.Exists(fullTestAbsolutePath))
            {
                File.WriteAllText(
                    fullTestAbsolutePath,
                    $"ID{delimiter}Order{delimiter}Type{delimiter}Variable Name{delimiter}Value{delimiter}Condition{delimiter}" +
                        $"High Lim{delimiter}Low Lim{delimiter}Time [ms]{delimiter}Timeout [ms]{delimiter}Skip{delimiter}Description" +
                            $"{Environment.NewLine}"
                );
            }

            // Append the sub-test to the full-test file
            string[] instructions = File.ReadAllLines(subTestPath);
            n = instructions.Length - 1; // Remove headers

            // i = 1 -> Skip header
            for (int i = 1; i < n + 1; i++)
            {
                // Prepend the incremental id and order to the instruction
                string line = $"{counter + i - 1}{delimiter}{counter + i - 1}{delimiter}{instructions[i]}{Environment.NewLine}";
                File.AppendAllText(fullTestAbsolutePath, line);
            }

            return n;
        }

        /// <summary>
        /// Load a test program from disk
        /// </summary>
        /// <param name="path">The test program path</param>
        /// <param name="delimeter">The delimiter <see cref="char"/></param>
        /// <returns>A <see cref="List"/> with the retrieved <see cref="Instruction"/></returns>
        internal static List<Instruction> ReadTest(string path, char delimiter = '\t')
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
                string instructionType = testParsed[i][2].Trim();
                string variableName = testParsed[i][3].Trim();

                double value = 0d;
                int canId = 0;
                byte[] payload = new byte[8];
                if (instructionType.CompareTo("CAN_RAW") != 0)
                {
                    double.TryParse(
                        testParsed[i][4].TrimEnd(),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out value
                    );
                }
                else
                {
                    string valueAsString = testParsed[i][4];
                    string[] valueAsStringSplitted = valueAsString.Split('|');
                    canId = Convert.ToInt32(valueAsStringSplitted[0], 16);

                    string[] payloadAsString = valueAsStringSplitted[1].Split(' ');
                    for (int j = 0; j < 8; j++)
                        payload[j] = Convert.ToByte(payloadAsString[j], 16);
                }

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
                string description = "";
                try
                {
                    description = testParsed[i][11].Trim();
                }
                catch { }

                if (!skip)
                {
                    switch (instructionType)
                    {
                        case "GET":
                            instruction = new Get(variableName, id, order, timeout, description);
                            break;

                        case "SET":
                            instruction = new Set(variableName, value, id, order, timeout, description);
                            break;

                        case "WAIT":
                            instruction = new Wait(time, id, order, description);
                            break;

                        case "TEST":
                            instruction = new Test(variableName, id, order, value, ParseOperand(condition), description);
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
                                description: description
                            );
                            break;

                        case "CAN_RAW":
                            instruction = new CanRaw(
                                id,
                                order,
                                canId,
                                payload,
                                description: description
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

                case "<>":
                    operand = ConditionOperand.Included;
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
            {
                File.WriteAllText(
                    path,
                    $"User: {UserName}{Environment.NewLine}" +
                    $"Production site: {ProductionSite}{Environment.NewLine}" +
                    $"Batch number: {BatchNumber}{Environment.NewLine}" +
                    $"Serial number: {SerialNumbers.SerialNumbers.CreateNew(ProductionSite, SerialIndex)}{Environment.NewLine}" +
                    $"{Environment.NewLine}"
                );

                File.AppendAllText(
                    path,
                    $"Name\tID\tOrder\tVariable involved\tValue\tCondition to verify\tStart time\tStop time\tResult{Environment.NewLine}"
                );
            }
        }

        /// <summary>
        /// Finalize the result file with the last necessary lines (test passed or failed and MD5 hash)
        /// </summary>
        /// <param name="path">THe file path</param>
        /// <param name="testResult">The test result (<see langword="true"/> if passed, <see langword="false"/> otherwise</param>
        public static void FinalizeFile(string path, bool testResult)
        {
            // Test passed
            string testResultAsString = testResult ? "Passed" : "Failed";
            File.AppendAllText(path, $"{Environment.NewLine}Result: {testResultAsString}");

            // MD5 calculus and prepend (first line of the file)
            string text = File.ReadAllText(path);
            string encryptResult = Cryptography.MD5.CreateNew(text);

            File.WriteAllText(path, $"{encryptResult}{Environment.NewLine}{text}");
        }
    }
}