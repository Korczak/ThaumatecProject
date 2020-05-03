using NodaTime;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Device.GetUserDevices
{
    public class GetUserDeviceItem
    {
        public string Name { get; }
        public LocalDateTime LastUpdateDateTime { get; }
        public LocalDateTime LastPrintDateTime { get; }
        public string Location { get; }
        public DeviceStatus Status { get; }

        public GetUserDeviceItem(string name, LocalDateTime lastUpdateDateTime, LocalDateTime lastPrintDateTime, string location, DeviceStatus status)
        {
            Name = name;
            LastUpdateDateTime = lastUpdateDateTime;
            LastPrintDateTime = lastPrintDateTime;
            Location = location;
            Status = status;
        }
    }
}
