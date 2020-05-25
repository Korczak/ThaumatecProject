using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Thaumatec.Core.Print.Stop
{
    public class PrintStopRequest
    {
        public string SerialNumber { get; }
        public string PrintName { get; }

        public PrintStopRequest(string serialNumber, string printName)
        {
            SerialNumber = serialNumber;
            PrintName = printName;
        }
    }
}
