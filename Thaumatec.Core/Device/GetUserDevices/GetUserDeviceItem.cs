using NodaTime;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Device.GetUserDevices
{
    public class GetUserDeviceItem
    {
        public string Name { get; }
        public LocalDateTime LastUpdateDateTime { get; }
        public LocalDateTime LastPrintDateTime { get; }
        public DeviceStatus Status { get; }

        public GetUserDeviceItem(string name, LocalDateTime lastUpdateDateTime, LocalDateTime lastPrintDateTime, DeviceStatus status)
        {
            Name = name;
            LastUpdateDateTime = lastUpdateDateTime;
            LastPrintDateTime = lastPrintDateTime;
            Status = status;
        }
    }
}
