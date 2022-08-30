using Instructions.Extensions;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Instructions
{
    /// <summary>
    /// Program the MCU (send a batch file to be executed)
    /// </summary>
    public class ProgramMcu : Instruction
    {
        private string path;
        private int exitCode;

        /// <summary>
        /// Create a new instance of <see cref="ProgramMcu"/>
        /// </summary>
        /// <param name="path">The batch file path</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        /// <param name="description">The description</param>
        public ProgramMcu(string path, int id, int order, string description = "") : base("UploadFirmware", id, order, timeout: 0, description: description)
        {
            this.path = path;
        }

        public override async Task Execute()
        {
            FileInfo info = new FileInfo(path); // Working directory (same as script file)

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = path,
                WorkingDirectory = info.Directory.FullName
            };

            Process process = Process.Start(startInfo);
            await process.WaitForExitAsync();

            exitCode = process.ExitCode;
            result =  exitCode == 0;

            outputParameters.Add(exitCode);
        }

        public override string ToString()
        {
            string description = $"{name}\t{exitCode}";
            return description;
        }
    }
}