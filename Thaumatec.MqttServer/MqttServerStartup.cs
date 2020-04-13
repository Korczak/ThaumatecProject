using Serilog;
using System.IO;
using System.Reflection;
using Thaumatec.Core.Configuration;

namespace Thaumatec.MqttServer
{
    public class MqttServerStartup : IStartupStep
    {
        private readonly Config _config;

        public MqttServerService MqttServerService { get; }

        public MqttServerStartup(Config config)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var mqttServerLogger = new LoggerConfiguration()
               .MinimumLevel.Is(config.LogLevel)
               .WriteTo.File(Path.Combine(currentPath,
                   @"log\mqtt_server_.txt"), rollingInterval: RollingInterval.Minute)
               .CreateLogger();

            _config = config;
            MqttServerService = new MqttServerService(_config, mqttServerLogger);
        }

        public IStartupValidation Configure()
        {
            MqttServerService.Start();

            return new MqttServerValidation();
        }
    }
}
