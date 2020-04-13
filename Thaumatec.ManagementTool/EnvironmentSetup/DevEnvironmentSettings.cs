using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Thaumatec.ManagementTool.EnvironmentSetup
{
    internal class DevEnvironmentSettings
    {
        public string AppSettingsPath { get; }
        public string AppSettingsExamplePath { get; }
        public string MongoDbConnectionStringPath { get; }
        public string TestDatabaseConnectionStringPath { get; }

        public DevEnvironmentSettings(Solution solution)
        {
            var solutionRoot = solution.SolutionRoot;
            AppSettingsPath = Path.Combine(solutionRoot, "Web", "appsettings.json");
            AppSettingsExamplePath = Path.Combine(solutionRoot, "Web", "appsettings.example.json");
            MongoDbConnectionStringPath = Path.Combine(solutionRoot, "Core", "Database", "Models", "ConnectionString.txt");
            TestDatabaseConnectionStringPath = Path.Combine(solutionRoot, "DatabaseTests", "ConnectionString.txt");
        }
    }
}
