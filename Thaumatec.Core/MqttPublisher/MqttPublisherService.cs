using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.Mqtt;

namespace Thaumatec.Core.MqttPublisher
{
    public class MqttPublisherService
    {

        private readonly MqttClientConnection _mqttClient;
        private readonly IMqttClientOptions _mqttClientOptions;

        public MqttPublisherService(MqttClientSettings mqttClientSettings)
        {
            _mqttClient = new MqttClientConnection();
            _mqttClientOptions = new MqttClientOptionsBuilder()
               .WithTcpServer(mqttClientSettings.Host, mqttClientSettings.Port)
               .WithCredentials(mqttClientSettings.UserName, mqttClientSettings.Password)
               .Build();
        }

        public void SetConnection()
        {
            _mqttClient.SetConnection(_mqttClientOptions, OnMessage).Wait();
        }

        public void OnMessage(MqttApplicationMessageReceivedEventArgs message)
        {
        }

        public async Task SendMessage(MqttApplicationMessage message)
        {
            await _mqttClient.Client.PublishAsync(message);
        }

    }
}
