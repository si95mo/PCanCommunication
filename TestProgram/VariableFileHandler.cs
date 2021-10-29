using DataStructures.VariablesDictionary;
using System;
using System.Globalization;
using System.IO;

namespace TestProgram
{
    internal class VariableFileHandler
    {
        internal static void ReadTest(string path, char delimeter = ';')
        {
            string[] testProgram = File.ReadAllLines(path);
            string[][] testParsed = new string[testProgram.Length][];

            DoubleVariable variable;
            uint index, subIndex;
            string name, description, measureUnit;
            VariableType type;
            double scale, offset;
            for (int i = 1; i < testProgram.Length; i++) // No headers
            {
                testParsed[i] = testProgram[i].Split(';');

                uint.TryParse(testParsed[i][0].Trim().TrimEnd(), out index);
                uint.TryParse(testParsed[i][1].Trim().TrimEnd(), out subIndex);
                name = testParsed[i][2];
                description = testParsed[i][3].Trim().TrimEnd();
                Enum.TryParse(testParsed[i][4].Trim().TrimEnd(), out type);
                measureUnit = testParsed[i][5].Trim().TrimEnd();
                double.TryParse(testParsed[i][6].Trim().TrimEnd(), NumberStyles.Any, CultureInfo.InvariantCulture, out scale);
                double.TryParse(testParsed[i][7].Trim().TrimEnd(), NumberStyles.Any, CultureInfo.InvariantCulture, out offset);

                variable = new DoubleVariable(name, index, subIndex, type, description, measureUnit, scale, offset);
                VariableDictionary.Add(variable);
            }
        }
    }
}