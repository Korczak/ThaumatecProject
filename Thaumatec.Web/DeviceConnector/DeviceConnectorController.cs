using MongoDB.Bson;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Thaumatec.Core.DeviceConnector.ActualStatus;
using Thaumatec.Core.DeviceConnector.Initialize;
using Thaumatec.Core.DeviceConnector.PrintAbort;
using Thaumatec.Core.DeviceConnector.PrintEnd;
using Thaumatec.Core.DeviceConnector.PrintStatus;
using Thaumatec.Core.Mqtt;

namespace Thaumatec.Web.DeviceConnector
{
    public class DeviceConnectorController : MqttBaseController
    {
        private readonly DeviceConnectorInitializeService _initializeService;
        private readonly DeviceConnectorActualStatusService _actualStatusService;
        private readonly DeviceConnectorPrintStatusService _deviceConnectorPrintStatusService;
        private readonly DeviceConnectorPrintEndAccess _deviceConnectorPrintEndAccess;
        private readonly DeviceConnectorPrintAbortAccess _deviceConnectorPrintAbortAccess;
        private readonly MqttClientConnection _mqttClient;

        public DeviceConnectorController(
            MqttClientSettings mqttClientSettings,
            DeviceConnectorInitializeService initializeService,
            DeviceConnectorActualStatusService actualStatusService,
            DeviceConnectorPrintStatusService deviceConnectorPrintStatusService,
            DeviceConnectorPrintEndAccess deviceConnectorPrintEndAccess,
            DeviceConnectorPrintAbortAccess deviceConnectorPrintAbortAccess)
        {
            _initializeService = initializeService;
            _actualStatusService = actualStatusService;
            _deviceConnectorPrintStatusService = deviceConnectorPrintStatusService;
            _deviceConnectorPrintEndAccess = deviceConnectorPrintEndAccess;
            _deviceConnectorPrintAbortAccess = deviceConnectorPrintAbortAccess;

            var mqttClientOptions = new MqttClientOptionsBuilder()
               .WithTcpServer(mqttClientSettings.Host, mqttClientSettings.Port)
               .WithCredentials(mqttClientSettings.UserName, mqttClientSettings.Password)
               .Build();

            _mqttClient = new MqttClientConnection();
            _mqttClient.SetConnection(mqttClientOptions, OnMessage).Wait();

            _mqttClient.Client.SubscribeAsync("Thaumatec/Printer/#").Wait();
        }

        public void OnMessage(MqttApplicationMessageReceivedEventArgs message)
        {
            var topic = message.ApplicationMessage.Topic;

            if (Regex.IsMatch(topic, "Thaumatec/Printer/Init"))
            {
                Initialize(message.ApplicationMessage);
            }
            else if (Regex.IsMatch(topic, @"Thaumatec/Printer/[^/]*/Status"))
            {
                ActualStatus(message.ApplicationMessage);
            }
            else if (Regex.IsMatch(topic, @"Thaumatec/Printer/[^/]*/Print/Status"))
            {
                PrintStatus(message.ApplicationMessage);
            }
            else if (Regex.IsMatch(topic, "Thaumatec/Printer/[^/]*/Print/Status/Image"))
            {
                PrintImageStatus(message.ApplicationMessage);
            }
            else if (Regex.IsMatch(topic, "Thaumatec/Printer/[^/]*/Print/End"))
            {
                PrintEnd(message.ApplicationMessage);
            }
            else if (Regex.IsMatch(topic, "Thaumatec/Printer/[^/]*/Print/Abort"))
            {
                PrintAbort(message.ApplicationMessage);
            }
        }

        public void Initialize(MqttApplicationMessage message)
        {
            var request = JsonConvert.DeserializeObject<DeviceConnectorInitializeRequest>(Encoding.Default.GetString(message.Payload));

            var response = _initializeService.InitializeDevice(request).GetAwaiter().GetResult();
            var responseJson = JsonConvert.SerializeObject(response);
            var responseTopic = message.ResponseTopic;

            _mqttClient.Client.PublishAsync(responseTopic, responseJson, true);
        }

        public void ActualStatus(MqttApplicationMessage message)
        {
            var request = JsonConvert.DeserializeObject<DeviceConnectorActualStatusRequest>(Encoding.Default.GetString(message.Payload));

            var serialNumber = message.Topic.Split('/')[2];

            _actualStatusService.UpdateStatus(request, serialNumber).Wait();
        }


        public void PrintImageStatus(MqttApplicationMessage message)
        {
        }

        public void PrintStatus(MqttApplicationMessage message)
        {
            var request = JsonConvert.DeserializeObject<DeviceConnectorPrintStatusRequest>(Encoding.Default.GetString(message.Payload));

            var serialNumber = message.Topic.Split('/')[2];

            _deviceConnectorPrintStatusService.UpdateStatus(request, serialNumber).Wait();
        }

        public void PrintAbort(MqttApplicationMessage message)
        {
            var request = JsonConvert.DeserializeObject<DeviceConnectorPrintAbortRequest>(Encoding.Default.GetString(message.Payload));

            var serialNumber = message.Topic.Split('/')[2];

            _deviceConnectorPrintAbortAccess.EndPrint(serialNumber, request).Wait();
        }

        public void PrintEnd(MqttApplicationMessage message)
        {
            var request = JsonConvert.DeserializeObject<DeviceConnectorPrintEndRequest>(Encoding.Default.GetString(message.Payload));

            var serialNumber = message.Topic.Split('/')[2];

            _deviceConnectorPrintEndAccess.EndPrint(serialNumber, request).Wait();
        }
    }
}
