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
        public string Name { get; }
        public string Location { get; }

        public AppendDeviceToUserInput(string serialNumber, string username, string name, string location)
        {
            SerialNumber = serialNumber;
            Username = username;
            Name = name;
            Location = location;
        }
    }
}
