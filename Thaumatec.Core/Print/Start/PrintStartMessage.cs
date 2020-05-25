using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Print.Start
{
    public class PrintStartMessage
    {
        [JsonProperty(PropertyName = "command")]
        public string Command { get; }
        [JsonProperty(PropertyName = "gcode")]
        public string Gcode { get; }

        public PrintStartMessage(string command, string gcode)
        {
            Command = command;
            Gcode = gcode;
        }
    }
}
