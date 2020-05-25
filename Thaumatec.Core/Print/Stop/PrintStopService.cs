using MQTTnet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.MqttPublisher;

namespace Thaumatec.Core.Print.Stop
{
    public class PrintStopService
    {
        private readonly PrintStopAccess _access;
        private readonly MqttPublisherService _publisherService;

        public PrintStopService(PrintStopAccess access, MqttPublisherService publisherService)
        {
            _access = access;
            _publisherService = publisherService;
        }

        public async Task<PrintStopResponse> StopPrint(PrintStopRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var status = await _access.GetDeviceStatus(request.SerialNumber);

            if (status != Device.Constants.DeviceStatus.Printing)
                return PrintStopResponse.Failure();

            await _access.StopPrint(request.SerialNumber, request.PrintName);

            var payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new PrintStopMessage()));

            var message = new MqttApplicationMessage()
            {
                ContentType = MediaTypeNames.Application.Json,
                Payload = payload,
                Topic = $"Thaumatec/Printer/{request.SerialNumber}/device"
            };

            await _publisherService.SendMessage(message);
            return PrintStopResponse.Success();
        }
    }
}
