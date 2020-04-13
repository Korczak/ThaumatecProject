using Thaumatec.Core.Configuration;
using Thaumatec.Core.Database.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Thaumatec.Web.Configuration;
using System;
using Serilog.Events;
using Thaumatec.Core.Logging;
using Mongo.Migration.Startup.DotNetCore;
using Mongo.Migration.Startup;

namespace Thaumatec.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        private RuntimeStatus _runtimeStatus;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _runtimeStatus = new RuntimeStatus();
            var (config, configValidation) = GetConfig(Configuration);

            services
                .AddMvc(x =>
                {
                    x.Filters.Add<RuntimeValidationFilter>();
                    x.EnableEndpointRouting = false;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Frontend/dist";
            });
            
            services.AddMigration(new MongoMigrationSettings
            {
                ConnectionString = config.ConnectionString,
                Database = config.DatabaseName,
                VersionFieldName = "Version" // Optional
            });

            services.AddSingleton(_runtimeStatus);
            services.AddSingleton<IClock>(SystemClock.Instance);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var (config, configValidation) = GetConfig(Configuration);

            var webStartup = new WebStartup(app, env);
            var loggingStartup = new LoggingStartup(config, new LoggingPaths());

            DatabaseConnection.SetConnection(config);

            var webValidation = webStartup.Configure();
            var loggingValidation = loggingStartup.Configure();

            _runtimeStatus.Update(
                configValidation,
                webValidation,
                loggingValidation
                );

            PrintStatus();
        }

        private (Config config, ConfigurationValidation configurationValidation) GetConfig(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>(nameof(Config.ConnectionString));
            var databaseName = configuration.GetValue<string>(nameof(Config.DatabaseName));
            var allowHosts = configuration.GetValue<string>(nameof(Config.AllowedHosts));
            var serverAddress = configuration.GetValue<string>(nameof(Config.Urls));
            var logLevel = configuration.GetValue<LogEventLevel>(nameof(Config.LogLevel));
            var config = new Config(connectionString, databaseName, allowHosts, logLevel, serverAddress);

            var validation = config.Validate();

            return (config: config, configurationValidation: validation);
        }

        private void PrintStatus()
        {
            WriteLineColor("System status:", ConsoleColor.White);
            Console.WriteLine();

            foreach(var validation in _runtimeStatus.AllValidations)
            {
                Console.Write($"{validation.DisplayName} - ");
                if(validation.Success)
                {
                    WriteLineColor("OK", ConsoleColor.Green);
                }
                else
                {
                    WriteLineColor("ERROR", ConsoleColor.Red);
                    foreach (var error in validation.GetErrors())
                    {
                        Console.WriteLine($"\t{error}");
                    }
                }
            }
        }

        private void WriteLineColor(string text, ConsoleColor? foreground, ConsoleColor? background = null)
        {
            var originalForeground = Console.ForegroundColor;
            var originalBackground = Console.BackgroundColor;

            Console.ForegroundColor = foreground ?? originalForeground;
            Console.BackgroundColor = background ?? originalBackground;

            Console.WriteLine(text);
            
            Console.ForegroundColor = originalForeground;
            Console.BackgroundColor = originalBackground;
        }
    }
}
