using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Device.AppendDeviceToUser
{
    public class AppendDeviceToUserInput
    {
        public string SerialNumber { get; }
        public string Username{ get; }

        public AppendDeviceToUserInput(string serialNumber, string username)
        {
            SerialNumber = serialNumber;
            Username = username;
        }
    }
}
