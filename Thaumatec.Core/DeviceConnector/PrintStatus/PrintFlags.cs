using Newtonsoft.Json;

namespace Thaumatec.Core.DeviceConnector.PrintStatus
{
    public class PrintFlags
    {
        [JsonProperty(PropertyName = "operational")]
        public bool Operational { get; set; }
        [JsonProperty(PropertyName = "paused")]
        public bool Paused { get; set; }
        [JsonProperty(PropertyName = "printing")]
        public bool Printing{ get; set; }
        [JsonProperty(PropertyName = "cancelling")]
        public bool Cancelling { get; set; }
        [JsonProperty(PropertyName = "pausing")]
        public bool Pausing { get; set; }
        [JsonProperty(PropertyName = "sdReady")]
        public bool SDReady{ get; set; }
        [JsonProperty(PropertyName = "error")]
        public bool Error { get; set; }
        [JsonProperty(PropertyName = "ready")]
        public bool Ready { get; set; }
        [JsonProperty(PropertyName = "closedOnError")]
        public bool ClosedOnError { get; set; }
    }
}