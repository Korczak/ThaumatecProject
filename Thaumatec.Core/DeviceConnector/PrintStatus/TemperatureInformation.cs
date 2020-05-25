using Newtonsoft.Json;

namespace Thaumatec.Core.DeviceConnector.PrintStatus
{
    public class TemperatureInformation
    {
        [JsonProperty(PropertyName = "tool0")]
        public TemperatureItemInformation TemperatureTool0 { get; set; }
        [JsonProperty(PropertyName = "tool1")]
        public TemperatureItemInformation TemperatureTool1 { get; set; }
        [JsonProperty(PropertyName = "bed")]
        public TemperatureItemInformation TemperatureBed { get; set; }
    }
}