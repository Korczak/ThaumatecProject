using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Thaumatec.Core.Device.AddNewDevice
{
    public class AddNewDeviceService
    {
        private readonly AddNewDeviceAccess _access;

        public AddNewDeviceService(AddNewDeviceAccess access)
        {
            _access = access;
        }

        public Task AddNewDevice(AddNewDeviceRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return InternalAddNewDevice(request);
        }

        private async Task InternalAddNewDevice(AddNewDeviceRequest request)
        {
            await _access.AddNewDevice(request);
        }
    }
}
