using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Print.Stop
{
    public class PrintStopMessage
    {
        [JsonProperty(PropertyName = "command")]
        public string Command { get; }

        public PrintStopMessage()
        {
            Command = "Stop print";
        }
    }
}
