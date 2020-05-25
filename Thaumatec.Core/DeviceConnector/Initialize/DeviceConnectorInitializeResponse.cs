using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.DeviceConnector.Initialize
{
    public class DeviceConnectorInitializeResponse
    {
        [JsonProperty(PropertyName ="command")]
        public string Command { get; }
        [JsonProperty(PropertyName = "result")]
        public DeviceConnectorInitializeResult Result { get; }

        public DeviceConnectorInitializeResponse(string command, DeviceConnectorInitializeResult result)
        {
            Command = command;
            Result = result;
        }
    }
}
