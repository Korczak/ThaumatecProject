using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.DeviceConnector.ActualStatus;
using Thaumatec.Core.DeviceConnector.Initialize;
using Thaumatec.Core.Mqtt;

namespace Thaumatec.Web.DeviceConnector
{
    public class DeviceConnectorController : MqttBaseController
    {
        private readonly DeviceConnectorInitializeService _initializeService;
        private readonly DeviceConnectorActualStatusService _actualStatusService;
        private readonly MqttClientConnection _mqttClient;

        public DeviceConnectorController(IMqttClientOptions mqttClientOptions, DeviceConnectorInitializeService initializeService, DeviceConnectorActualStatusService actualStatusService)
        {
            _initializeService = initializeService;
            _actualStatusService = actualStatusService;

            _mqttClient = new MqttClientConnection();
            _mqttClient.SetConnection(mqttClientOptions, OnMessage).GetAwaiter();

            _mqttClient.Client.SubscribeAsync("Thaumatec/Printer/Init").GetAwaiter();
            _mqttClient.Client.SubscribeAsync("Thaumatec/Printer/+/Status").GetAwaiter();
        }

        public void OnMessage(MqttApplicationMessageReceivedEventArgs message)
        {
            var topic = message.ApplicationMessage.Topic;
            switch(topic)
            {
                case "Thaumatec/Printer/Init":
                    Initialize(message.ApplicationMessage);
                    break;
                case "Thaumatec/Printer/+/Status":
                    Initialize(message.ApplicationMessage);
                    break;
            }
        }

        public void Initialize(MqttApplicationMessage message)
        {
            var request = JsonConvert.DeserializeObject<DeviceConnectorInitializeRequest>(Encoding.Default.GetString(message.Payload));

            _initializeService.InitializeDevice(request).GetAwaiter();

            var responseTopic = message.ResponseTopic;
        }

        public void ActualStatus(MqttApplicationMessage message)
        {
            var request = JsonConvert.DeserializeObject<DeviceConnectorActualStatusRequest>(Encoding.Default.GetString(message.Payload));

            var serialNumber = message.Topic.Split('/')[2];

            _actualStatusService.UpdateStatus(request, serialNumber).GetAwaiter();

            var responseTopic = message.ResponseTopic;
        }
    }
}
