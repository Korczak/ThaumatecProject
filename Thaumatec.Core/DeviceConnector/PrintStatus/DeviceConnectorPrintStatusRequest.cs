using Newtonsoft.Json;
using NodaTime;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.DeviceConnector.PrintStatus
{
    public class DeviceConnectorPrintStatusRequest
    {
        [JsonProperty("date_time")]
        public LocalDateTime DateTime { get; set; }
        [JsonProperty("temperature")]
        public TemperatureInformation TemperatureInformation { get; set; }
        [JsonProperty("state")]
        public PrintState State { get; set; }
    }
}
