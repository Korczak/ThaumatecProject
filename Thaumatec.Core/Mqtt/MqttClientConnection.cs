using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using Serilog;
using System.Threading.Tasks;

namespace Thaumatec.Core.Mqtt
{
    public static class MqttClientConnection
    {
        public static IMqttClient Client { get => _client; }
        private static IMqttClient _client;
        private static bool _isConnectionEstablished = false;

        static MqttClientConnection()
        {
            _client = new MqttFactory().CreateMqttClient();
        } 

        public static async Task SetConnection(IMqttClientOptions options)
        {
            if (_isConnectionEstablished)
                return;

            await _client.ConnectAsync(options);
            if (!_client.IsConnected)
            {
                await ReconnectClient();
            }

            if (_client.IsConnected)
            {
                await _client.SubscribeAsync("hello/world");
                Log.Information("Client is connected");
            }
            else
            {
                Log.Error("Client could not connect");
            }

            _client.UseApplicationMessageReceivedHandler(OnMessage);
            _client.UseDisconnectedHandler(OnDisconnect);
        }

        public static void OnMessage(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            Log.Information("A message is received");
        }

        public static void OnDisconnect(MqttClientDisconnectedEventArgs eventArgs)
        {
            Log.Information("Client has disconnected");
            Log.Information("Trying to reconnect...");
            ReconnectClient().GetAwaiter();
            if (_client.IsConnected)
            {
                Log.Information("Client reconnected");
            }
            else
            {
                Log.Information("Could not reconnect");
            }
        }


        private static async Task ReconnectClient(int numTry = 30)
        {
            int numOfAttempts = 0;
            while (numOfAttempts < numTry)
            {
                await _client.ReconnectAsync();
                if (_client.IsConnected)
                    return;
            }
        }

    }
}
