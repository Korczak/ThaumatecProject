using Thaumatec.Core.Configuration;
using Thaumatec.Core.Database.Settings;
using Newtonsoft.Json;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.IO;

namespace Thaumatec.ManagementTool.EnvironmentSetup
{
    internal static class EnvironmentSetup
    {
        internal static void InitEnvironment()
        {
            if (!IsNpmAvailable())
            {
                Console.WriteLine("Npm is not available");
                return;
            }

            var solution = new Solution();
            if (!solution.Found)
            {
                Console.WriteLine("Solution not found");
                return;
            }

            var envSettings = new DevEnvironmentSettings(solution);
            var config = TryLoadConfig(envSettings);

            var connectionString = config?.ConnectionString ?? GetConnectionString();
            var databaseName = config?.DatabaseName ?? GetDatabaseString();
            var allowedHosts = config?.AllowedHosts ?? GetAllowedHosts();
            var logLevel = config?.LogLevel ?? GetLogLevel();
            var serverAddress = config?.Urls ?? GetServerAddress();

            config = new Config(connectionString, databaseName, allowedHosts, logLevel, serverAddress);

            Console.WriteLine($"Writing config values to {envSettings.AppSettingsPath}");
            File.WriteAllText(envSettings.AppSettingsPath, JsonConvert.SerializeObject(config, Formatting.Indented));
        }

        private static string GetConnectionString()
        {
            Console.WriteLine("Make sure you can access the database server and have the privileges to create databases.");
            Console.WriteLine("Then specify the connection settings.");
            return GetString("connectionString", "");            
        }

        private static string GetDatabaseString()
        {
            return GetString("databaseName", "");
        }

        private static string GetAllowedHosts()
        {
            Console.WriteLine("Specify the hosts the application will run on");
            string allowedHosts = GetString("allowed hosts", "*");
            return allowedHosts;
        }

        private static LogEventLevel GetLogLevel()
        {
            Console.WriteLine("Specify log level: [0:Verbose, 1:Debug, 2:Information, 3:Warning, 4:Error, 5:Fatal]");
            var defaultLogLevel = 1;
            var loglevel = GetInt("log level", defaultLogLevel);
            if (loglevel > 5 || loglevel < 0)
            {
                Console.WriteLine($"Value out for range. Set default value");
                return (LogEventLevel)defaultLogLevel;
            }
            return (LogEventLevel)loglevel;
        }

        private static string GetServerAddress()
        {
            Console.WriteLine("Specify server address");
            string serverAddress = GetString("server address", "http://localhost:5000");
            return serverAddress;
        }

        private static string GetString(string label, string defaultValue)
        {
            Console.Write($"{label} [{defaultValue}]:");
            string value = Console.ReadLine();

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            else
            {
                return value;
            }
        }

        private static int GetInt(string label, int defaultValue)
        {
            Console.Write($"{label} [{defaultValue}]:");
            string value = Console.ReadLine();

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            else
            {
                if (int.TryParse(value, out int intValue))
                {
                    return intValue;
                }
                else
                {
                    Console.WriteLine($"Unable to parse {value} as integer");
                    Environment.Exit(1);
                    return 0;
                }
            }
        }

        private static bool IsNpmAvailable()
        {
            try
            {
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "npm",
                    Arguments = "--version",
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


        private static void SetTestDatabase(DevEnvironmentSettings settings)
        {
            Console.WriteLine("Configuring test database");

            if (!File.Exists(settings.TestDatabaseConnectionStringPath))
            {
                string testConnectionString = GetConnectionString();
                File.WriteAllText(settings.TestDatabaseConnectionStringPath, testConnectionString);

                Console.WriteLine($"Creating connection string file for the test database in {settings.TestDatabaseConnectionStringPath}");
            }
        }

        private static Config TryLoadConfig(DevEnvironmentSettings envSettings)
        {
            if (File.Exists(envSettings.AppSettingsPath))
            {
                try
                {
                    var config = Config.FromFile(envSettings.AppSettingsPath);
                    return config;
                }
                catch (JsonException)
                {
                    Console.WriteLine("Couldnt load appsettings");
                }
            }
            return null;
        }
    }
}
