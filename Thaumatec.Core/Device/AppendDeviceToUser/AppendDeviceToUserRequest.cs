using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Device.AppendDeviceToUser
{
    public class AppendDeviceToUserRequest
    {
        public string SerialNumber { get; }

        public AppendDeviceToUserRequest(string serialNumber)
        {
            SerialNumber = serialNumber;
        }
    }
}
