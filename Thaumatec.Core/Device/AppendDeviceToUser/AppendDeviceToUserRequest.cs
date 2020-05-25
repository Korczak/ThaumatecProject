using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Device.AppendDeviceToUser
{
    public class AppendDeviceToUserRequest
    {
        public string Name { get; }
        public string Location { get; }
        public string SerialNumber { get; }

        public AppendDeviceToUserRequest(string name, string location, string serialNumber)
        {
            Name = name;
            Location = location;
            SerialNumber = serialNumber;
        }
    }
}
