using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Thaumatec.Core.Print.Start
{
    public class PrintStartRequest
    {
        public string SerialNumber { get; }
        public string PrintName { get; }
        public string Gcode { get; }

        public PrintStartRequest(string serialNumber, string printName, string gcode)
        {
            SerialNumber = serialNumber;
            PrintName = printName;
            Gcode = Encoding.UTF8.GetString(Convert.FromBase64String(gcode));
        }
    }
}
