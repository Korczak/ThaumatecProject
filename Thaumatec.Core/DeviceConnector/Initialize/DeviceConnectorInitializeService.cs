using System;
using System.Threading.Tasks;

namespace Thaumatec.Core.DeviceConnector.Initialize
{
    public class DeviceConnectorInitializeService
    {
        private readonly DeviceConnectorInitializeAccess _access;

        public DeviceConnectorInitializeService(DeviceConnectorInitializeAccess access)
        {
            _access = access;
        }

        public async Task InitializeDevice(DeviceConnectorInitializeRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var printerId = _access.GetDeviceWithSerialNumber(request.SerialNumber);
            if(printerId == default)
                return;

            await _access.RegisterNewDevice(request.SerialNumber);
        }
    }
}
