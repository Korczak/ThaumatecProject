using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Thaumatec.Core.DeviceConnector.PrintStatus
{
    public class DeviceConnectorPrintStatusService
    {
        private readonly DeviceConnectorPrintStatusAccess _access;

        public DeviceConnectorPrintStatusService(DeviceConnectorPrintStatusAccess access)
        {
            _access = access;
        }

        public async Task UpdateStatus(DeviceConnectorPrintStatusRequest request, string serialNumber)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (serialNumber == null) throw new ArgumentNullException(nameof(serialNumber));

            var updated = new PrintStatusUpdated(
                request.TemperatureInformation.TemperatureBed.Actual, 
                request.TemperatureInformation.TemperatureTool0.Actual, 
                request.TemperatureInformation.TemperatureTool1.Actual);

            await _access.UpdatePrintStatus(serialNumber, updated);
            
        }
    }
}
