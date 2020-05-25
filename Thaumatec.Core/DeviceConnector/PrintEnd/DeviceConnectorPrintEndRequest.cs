using Newtonsoft.Json;
using NodaTime;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.DeviceConnector.PrintEnd
{
    public class DeviceConnectorPrintEndRequest
    {
        [JsonProperty(PropertyName = "command")]
        public LocalDateTime DateTime { get; set; }
        [JsonProperty(PropertyName = "printTime")]
        public LocalDateTime PrintTime { get; set; }
    }
}
