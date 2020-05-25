using Newtonsoft.Json;

namespace Thaumatec.Core.DeviceConnector.PrintStatus
{
    public class TemperatureItemInformation
    {
        [JsonProperty(PropertyName = "actual")]
        public double? Actual { get; set; }
        [JsonProperty(PropertyName = "target")]
        public double? Target{ get; set; }
        [JsonProperty(PropertyName = "offset")]
        public double? Offset { get; set; }
    }
}