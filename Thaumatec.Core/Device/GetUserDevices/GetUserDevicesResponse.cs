using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Thaumatec.Core.Device.GetUserDevices
{
    public class GetUserDevicesResponse
    {
        public bool Success { get; }
        public ImmutableList<GetUserDeviceItem> Devices { get; }

        private GetUserDevicesResponse(bool success, IEnumerable<GetUserDeviceItem> devices)
        {
            Success = success;
            Devices = devices.ToImmutableList();
        }

        public static GetUserDevicesResponse Successfull(IEnumerable<GetUserDeviceItem> devices) => new GetUserDevicesResponse(true, devices);
        public static GetUserDevicesResponse Failure() => new GetUserDevicesResponse(false, null);

    }
}
