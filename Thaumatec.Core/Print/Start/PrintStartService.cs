using MQTTnet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.MqttPublisher;

namespace Thaumatec.Core.Print.Start
{
    public class PrintStartService
    {
        private readonly PrintStartAccess _access;
        private readonly MqttPublisherService _publisherService;

        public PrintStartService(PrintStartAccess access, MqttPublisherService publisherService)
        {
            _access = access;
            _publisherService = publisherService;
        }

        public async Task<PrintStartResponse> StartPrint(PrintStartRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var status = await _access.GetDeviceStatus(request.SerialNumber);

            if (status != Device.Constants.DeviceStatus.Active)
                return PrintStartResponse.Failure();

            await _access.StartNewPrint(
                request.SerialNumber, 
                request.PrintName, 
                request.Gcode);

            var toDevice = new PrintStartMessage("Start print", request.Gcode);
            var payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(toDevice));
            var message = new MqttApplicationMessage()
            {
                ContentType = MediaTypeNames.Application.Json,
                Payload = payload,
                Topic = $"Thaumatec/Printer/{request.SerialNumber}/device",
                QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce
            };

            await _publisherService.SendMessage(message);
            return PrintStartResponse.Success();
        }
    }
}
