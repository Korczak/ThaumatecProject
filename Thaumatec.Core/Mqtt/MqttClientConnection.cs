using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Thaumatec.Core.Mqtt
{
    public class MqttClientConnection
    {
        public IMqttClient Client { get => _client; }
        private IMqttClient _client;
        private bool _isConnectionEstablished = false;

        public MqttClientConnection()
        {
            _client = new MqttFactory().CreateMqttClient();
        } 

        public async Task SetConnection(IMqttClientOptions options, Action<MqttApplicationMessageReceivedEventArgs> onMessage)
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
                Log.Information("Client is connected");
            }
            else
            {
                Log.Error("Client could not connect");
            }
            _client.UseApplicationMessageReceivedHandler(onMessage);
            _client.UseDisconnectedHandler(OnDisconnect);
        }

        public void OnDisconnect(MqttClientDisconnectedEventArgs eventArgs)
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


        private async Task ReconnectClient(int numTry = 30)
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
