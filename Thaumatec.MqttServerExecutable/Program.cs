using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Serilog;
using PeterKottas.DotNetCore.WindowsService;

namespace Thaumatec.MqttServerExecutable
{
    public class Program
    {
        public static void Main()
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var configSettings = ReadConfiguration(currentPath);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(configSettings.LogLevel)
                .WriteTo.File(Path.Combine(currentPath,
                    @"log\mqtt_server_.txt"), rollingInterval: RollingInterval.Minute)
                .WriteTo.Console()
                .CreateLogger();


            ServiceRunner<MqttServerService>.Run(config =>
            {
                var name = "Thaumatec MQTT Server";
                config.SetName("Thaumatec.MQTTServer");
                config.SetDisplayName(name);
                config.SetDescription("Creates mqtt server");
                config.Service(serviceConfig =>
                {
                    serviceConfig.ServiceFactory((svc, extraArguments) =>
                    {
                        return new MqttServerService(configSettings);
                    });
                    serviceConfig.OnStart((service, extraArguments) =>
                    {
                        service.Start();
                        Log.Information("Service {Name} started", name);
                    });

                    serviceConfig.OnStop(service =>
                    {
                        service?.Stop();
                        Log.Information("Service {Name} stopped", name);
                    });

                    serviceConfig.OnInstall(service =>
                    {
                        Log.Information("Service {Name} installed", name);
                    });

                    serviceConfig.OnUnInstall(service =>
                    {
                        Log.Information("Service {Name} uninstalled", name);
                    });

                    serviceConfig.OnPause(service =>
                    {
                        Log.Information("Service {Name} paused", name);
                    });

                    serviceConfig.OnContinue(service =>
                    {
                        Log.Information("Service {Name} continued", name);
                    });

                    serviceConfig.OnError(e =>
                    {
                        Log.Error(e, "Service {Name} errored with exception", name);
                    });
                });
            });
        }
        private static MqttServerConfig ReadConfiguration(string currentPath)
        {
            var filePath = $"{currentPath}\\config.json";

            MqttServerConfig config = null;

            if (File.Exists(filePath))
            {
                using var r = new StreamReader(filePath);
                var json = r.ReadToEnd();
                config = JsonConvert.DeserializeObject<MqttServerConfig>(json);
            }

            return config;
        }
    }
}