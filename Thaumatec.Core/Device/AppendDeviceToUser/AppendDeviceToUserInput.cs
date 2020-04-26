using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Device.AppendDeviceToUser
{
    public class AppendDeviceToUserInput
    {
        public string SerialNumber { get; }
        public ObjectId UserId { get; }

        public AppendDeviceToUserInput(string serialNumber, ObjectId userId)
        {
            SerialNumber = serialNumber;
            UserId = userId;
        }
    }
}
