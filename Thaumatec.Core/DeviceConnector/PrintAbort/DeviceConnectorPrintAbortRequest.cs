using Newtonsoft.Json;
using NodaTime;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.DeviceConnector.PrintAbort
{
    public class DeviceConnectorPrintAbortRequest
    {
        [JsonProperty(PropertyName = "command")]
        public string Command { get; set; }
    }
}
