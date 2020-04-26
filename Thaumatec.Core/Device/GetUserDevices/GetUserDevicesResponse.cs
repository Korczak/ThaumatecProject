using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Thaumatec.Core.Device.GetUserDevices
{
    public class GetUserDevicesResponse
    {
        public ImmutableList<GetUserDeviceItem> Devices { get; }

        public GetUserDevicesResponse(IEnumerable<GetUserDeviceItem> devices)
        {
            Devices = devices.ToImmutableList();
        }
    }
}
