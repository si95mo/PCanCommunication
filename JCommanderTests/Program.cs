using Instructions.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace JCommanderTests
{
    internal class Program
    {
        //private static readonly string ExePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "J-Link Commander V7.70c.lnk");
        private static readonly string ExePath = @"C:\Program Files\SEGGER\JLink\JLink.exe";

        private static readonly string BatchPath = @"C:\Users\simod\Desktop\Meta\Lavori\AqTest\Batch\test.bat";

        private static async Task Main()
        {
            FileInfo info = new FileInfo(BatchPath);

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = BatchPath,
                WorkingDirectory = info.Directory.FullName
            };

            Process process = Process.Start(startInfo);
            await process.WaitForExitAsync();

            Console.WriteLine(
                $"Exit code: {process.ExitCode}{Environment.NewLine}" +
                $"--------------------------------"
            );

            Console.Write("Press 'ENTER' to exit application: ");
            Console.ReadLine();
        }
    }
}