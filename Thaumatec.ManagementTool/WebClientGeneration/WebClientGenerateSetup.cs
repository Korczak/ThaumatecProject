using System;
using System.Diagnostics;
using System.IO;

namespace Thaumatec.ManagementTool.WebClientGeneration
{
    internal static class WebClientGenerateSetup
    {
        public static void GenerateWebClient()
        {
            if(!IsNswagAvailable())
            {
                Console.WriteLine("Nswag is not available, Please make sure nswag is installed");
                return;
            }

            var solution = new Solution();
            if(!solution.Found)
            {
                Console.WriteLine("Solution cannot be found");
                return;
            }

            var settings = new WebClientGenerateSettings(solution);

            if(!File.Exists(settings.NswagConfigPath)) {
                Console.WriteLine("Nswag file cannot be found");
                return;
            }
            Console.WriteLine("Generating web client by nswag");
            NswagRun($"run { settings.NswagConfigPath } /runtime:NetCore31");
        }

        private static void NswagRun(string command)
        {
            try
            {
                File.WriteAllText("nswag_run_temp.bat", $"@echo off\r\ndotnet-nswag {command}");

                var startInfo = new ProcessStartInfo()
                {
                    FileName = "nswag_run_temp.bat",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(startInfo).WaitForExit();
            }
            catch
            {
                NswagRunFallback(command);
            }
            finally
            {
                File.Delete("nswag_run_temp.bat");
            }
        }

        private static void NswagRunFallback(string command)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = "dotnet-nswag",
                Arguments = command,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(startInfo).WaitForExit();
        }

        private static bool IsNswagAvailable()
        {
            try
            {
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "dotnet-nswag",
                    Arguments = "version",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(startInfo).WaitForExit();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
