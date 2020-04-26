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
using Thaumatec.Core.Mqtt;
using Thaumatec.MqttServer;
using System.Reflection;
using System.Linq;

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
            
            services.AddSingleton(_runtimeStatus);
            services.AddSingleton<IClock>(SystemClock.Instance);

            AutoConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var (config, configValidation) = GetConfig(Configuration);

            var webStartup = new WebStartup(app, env);
            var loggingStartup = new LoggingStartup(config, new LoggingPaths());
            var mqttServerStartup = new MqttServerStartup(config);
            var mqttClientStartup = new MqttClientStartup(config);
            var databaseStartup = new DatabaseStartup(config);

            var webValidation = webStartup.Configure();
            var loggingValidation = loggingStartup.Configure();
            var mqttServerValidation = mqttServerStartup.Configure();
            var mqttClientValidation = mqttClientStartup.Configure();
            var databaseValidation = databaseStartup.Configure();

            _runtimeStatus.Update(
                configValidation,
                webValidation,
                loggingValidation,
                mqttServerValidation,
                mqttClientValidation,
                databaseValidation
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
            var brokerHostSettings = new MqttBrokerHostSettings();
            configuration.GetSection(nameof(MqttBrokerHostSettings)).Bind(brokerHostSettings);
            var clientSettings = new MqttClientSettings();
            configuration.GetSection(nameof(MqttClientSettings)).Bind(clientSettings);

            var config = new Config(connectionString, databaseName, allowHosts, logLevel, serverAddress, clientSettings, brokerHostSettings);

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

        private void AutoConfigureServices(IServiceCollection services)
        {
            var serviceStartupTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IServiceStartup).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToArray();

            foreach (var type in serviceStartupTypes)
            {
                var instance = (IServiceStartup)Activator.CreateInstance(type);
                instance.ConfigureServices(services);
            }
        }
    }
}
