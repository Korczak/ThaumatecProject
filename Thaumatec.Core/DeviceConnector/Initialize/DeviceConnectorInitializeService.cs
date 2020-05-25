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

        public async Task<DeviceConnectorInitializeResponse> InitializeDevice(DeviceConnectorInitializeRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var printerId = _access.GetDeviceWithSerialNumber(request.SerialNumber);
            if(printerId != default)
                return new DeviceConnectorInitializeResponse("Init", DeviceConnectorInitializeResult.DeviceWithSerialNumberAlreadyExists);

            await _access.RegisterNewDevice(request.SerialNumber);
            return new DeviceConnectorInitializeResponse("Init", DeviceConnectorInitializeResult.Success);
        }
    }
}
