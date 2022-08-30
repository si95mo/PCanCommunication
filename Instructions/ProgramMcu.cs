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

        /// <summary>
        /// Create a new instance of <see cref="ProgramMcu"/>
        /// </summary>
        /// <param name="jLinkPath">The batch file path</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        /// <param name="timeout">The timeout (in milliseconds)</param>
        /// <param name="description">The description</param>
        public ProgramMcu(string path, int id, int order, string description = "") : base("UploadFirmware", id, order, timeout: 0, description: description)
        {
            this.path = path;
        }

        public override async Task Execute()
        {
            FileInfo info = new FileInfo(path);

            Process process = new Process();
            process.StartInfo.FileName = path;
            process.StartInfo.WorkingDirectory = info.Directory.FullName;

            process.Start();
            await process.WaitForExitAsync();
        }
    }
}