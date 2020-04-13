using Newtonsoft.Json;
using Serilog.Events;
using System.Collections.Generic;
using System.IO;

namespace Thaumatec.Core.Configuration
{
    public class Config
    {
        public string ConnectionString { get; }
        public string DatabaseName { get; }
        public string AllowedHosts { get; }
        public LogEventLevel LogLevel { get; }
        public string Urls { get; }

        public Config(string connectionString, string databaseName, string allowedHosts, LogEventLevel logLevel, string urls)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            AllowedHosts = allowedHosts;
            LogLevel = logLevel;
            Urls = urls;
        }

        public ConfigurationValidation Validate()
        {
            var errors = new List<string>();

            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                if(property.GetValue(this) is null)
                {
                    errors.Add(property.Name);
                }
            }

            if(errors.Count > 0)
            {
                return ConfigurationValidation.CreateError(errors);
            }
            else
            {
                return ConfigurationValidation.CreateSuccess();
            }
        }

        public static Config FromFile(string path)
        {
            var json = File.ReadAllText(path);
            var configuration = JsonConvert.DeserializeObject<Config>(json);
            return configuration;
        }
    }
}
