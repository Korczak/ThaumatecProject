using Newtonsoft.Json;

namespace Thaumatec.Core.DeviceConnector.Initialize
{
    public class DeviceConnectorInitializeRequest
    {
        [JsonProperty(PropertyName = "command")]
        public string Command { get; set; }
        [JsonProperty(PropertyName = "printerId")]
        public string SerialNumber { get; set; }
    }
}
