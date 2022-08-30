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
        /// <summary>
        /// The J-Link exe path
        /// </summary>
        public const string JLinkPath = @"C:\Program Files\SEGGER\JLink\JLink.exe";

        /// <summary>
        /// The command to send to the MCU
        /// </summary>
        public const string Command = "commanderscript";

        private string path;
        private string standardOutput, standardError;

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

            standardOutput = string.Empty;
            standardError = string.Empty;
        }

        public override async Task Execute()
        {
            FileInfo info = new FileInfo(path); // Working directory (same as script file)

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                UseShellExecute = false, // To redirect standard streams
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                FileName = path,
                WorkingDirectory = info.Directory.FullName
            };
            Process process = Process.Start(startInfo);

            process.Start();
            await process.WaitForExitAsync();

            standardOutput = await process.StandardOutput.ReadToEndAsync();
            standardError = await process.StandardError.ReadToEndAsync();

            result = process.ExitCode == 0 && standardError.CompareTo(string.Empty) == 0;

            outputParameters.Add(standardOutput);
            outputParameters.Add(standardError);
        }

        public override string ToString()
        {
            string description = $"{name}\t{standardOutput}\t{standardError}";
            return description;
        }
    }
}