using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Thaumatec.Core.DeviceConnector.ActualStatus
{
    public class DeviceConnectorActualStatusService
    {
        private readonly DeviceConnectorActualStatusAccess _access;

        public DeviceConnectorActualStatusService(DeviceConnectorActualStatusAccess access)
        {
            _access = access;
        }

        public async Task UpdateStatus(DeviceConnectorActualStatusRequest request, string serialNumber)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (serialNumber == null) throw new ArgumentNullException(nameof(serialNumber));

            await _access.UpdateDeviceStatus(request.Status, serialNumber);
        }
    }
}
