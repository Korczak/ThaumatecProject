using MQTTnet.Client;
using System.Threading.Tasks;

namespace Thaumatec.Core.Mqtt
{
    public class MqttClientHandler
    {
        public IMqttClient Client { get; }

        public MqttClientHandler()
        {
            Client = MqttClientConnection.Client;
        }

        public async Task SubscribeToTopic(string topic)
        {
            await Client.SubscribeAsync("hello/world");
        }
        public async Task UnsubscribeFromTopic(string topic)
        {
            await Client.UnsubscribeAsync("hello/world");
        }

        public async Task Publish(string topic, string payload)
        {
            await Client.PublishAsync(topic, payload);
        }

        public async Task StopClient()
        {
            await Client.DisconnectAsync();
        }
    }
}
