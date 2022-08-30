using Instructions.Extensions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JCommanderTests
{
    internal class Program
    {
        //private static readonly string ExePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "J-Link Commander V7.70c.lnk");
        private static readonly string ExePath = @"C:\Program Files\SEGGER\JLink\JLink.exe";

        private static async Task Main()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = ExePath,
                Arguments = "SLEEP 1000",
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };

            Process process = Process.Start(startInfo);
            await process.WaitForExitAsync();

            Console.WriteLine(
                $"Standard output: {process.StandardOutput.ReadToEnd()}{Environment.NewLine}" +
                $"Standard error: {process.StandardOutput.ReadToEnd()}{Environment.NewLine}" +
                $"-------------------------------------------------------"
            );

            Console.Write("Press 'ENTER' to exit application: ");
            Console.ReadLine();
        }
    }
}