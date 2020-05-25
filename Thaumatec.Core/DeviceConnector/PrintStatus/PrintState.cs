using Newtonsoft.Json;

namespace Thaumatec.Core.DeviceConnector.PrintStatus
{
    public class PrintState
    {
        [JsonProperty(PropertyName = "text")]
        public string State{ get; set; }
        [JsonProperty(PropertyName = "flags")]
        public PrintFlags PrintFlags { get; set; }
    }
}