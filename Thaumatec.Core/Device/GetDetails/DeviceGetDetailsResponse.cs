using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Device.GetDetails
{
    public class DeviceGetDetailsResponse
    {
        public string Name { get; }
        public LocalDateTime LastPrintDateTime{ get; }
        public string Location{ get; }
        public int NumberOfPrintsDone{ get; }
        public DeviceStatus Status{ get; }

        public DeviceGetDetailsResponse(string name, LocalDateTime lastPrintDateTime, string location, int numberOfPrintsDone, DeviceStatus status)
        {
            Name = name;
            LastPrintDateTime = lastPrintDateTime;
            Location = location;
            NumberOfPrintsDone = numberOfPrintsDone;
            Status = status;
        }
    }
}
