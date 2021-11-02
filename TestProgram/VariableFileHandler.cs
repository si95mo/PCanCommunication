using DataStructures.VariablesDictionary;
using System;
using System.Globalization;
using System.IO;

namespace TestProgram
{
    internal class VariableFileHandler
    {
        internal static void ReadTest(string path, char delimiter = ';')
        {
            string[] testProgram = File.ReadAllLines(path);
            string[][] testParsed = new string[testProgram.Length][];

            DoubleVariable variable;
            string name, description, measureUnit;
            for (int i = 1; i < testProgram.Length; i++) // No headers
            {
                testParsed[i] = testProgram[i].Split(delimiter);

                byte.TryParse(testParsed[i][0].Trim().TrimEnd(), out byte index);
                ushort.TryParse(testParsed[i][1].Trim().TrimEnd(), out ushort subIndex);
                name = testParsed[i][2];
                description = testParsed[i][3].Trim().TrimEnd();
                Enum.TryParse(testParsed[i][4].Trim().TrimEnd(), out VariableType type);
                measureUnit = testParsed[i][5].Trim().TrimEnd();
                double.TryParse(testParsed[i][6].Trim().TrimEnd(), NumberStyles.Any, CultureInfo.InvariantCulture, out double scale);
                double.TryParse(testParsed[i][7].Trim().TrimEnd(), NumberStyles.Any, CultureInfo.InvariantCulture, out double offset);

                variable = new DoubleVariable(name, index, subIndex, type, description, measureUnit, scale, offset);
                VariableDictionary.Add(variable);
            }
        }
    }
}