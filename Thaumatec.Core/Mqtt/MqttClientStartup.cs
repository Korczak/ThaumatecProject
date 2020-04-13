using System.Threading.Tasks;
using Thaumatec.Core.Configuration;
using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet;
using Serilog;

namespace Thaumatec.Core.Mqtt
{
    public class MqttClientStartup : IStartupStep
    {
        private readonly IMqttClientOptions _options;
        private IMqttClient _client;

        public MqttClientStartup(Config config)
        {
            _options = new MqttClientOptionsBuilder()
               .WithClientId(config.ClientSettings.Id)
               .WithTcpServer(config.BrokerSettings.Host, config.BrokerSettings.Port)
               .WithCredentials(config.ClientSettings.UserName, config.ClientSettings.Password)
               .Build();
        }

        public IStartupValidation Configure()
        {
            MqttClientConnection.SetConnection(_options).GetAwaiter();

            return new MqttClientValidation();
        }
    }
}
