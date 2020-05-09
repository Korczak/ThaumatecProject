using Newtonsoft.Json;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.DeviceConnector.ActualStatus
{
    public class DeviceConnectorActualStatusRequest
    {
        [JsonProperty(PropertyName = "command")]
        public string Command { get; set; }
        [JsonProperty(PropertyName = "dateTime")]
        public LocalDateTime DateTime  { get; set; }
        [JsonProperty(PropertyName = "status")]
        public DeviceStatus Status { get; set; }
    }
}
