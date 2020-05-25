using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Thaumatec.Core.Print.Details
{
    public class PrintDetailsService
    {
        private readonly PrintDetailsAccess _access;

        public PrintDetailsService(PrintDetailsAccess access)
        {
            _access = access;
        }

        public async Task<PrintDetailsResponse> GetPrintDetails(string serialNumber)
        {
            var status = await _access.GetDeviceStatus(serialNumber);

            if (status != Device.Constants.DeviceStatus.Printing)
                return PrintDetailsResponse.Failure();

            var printInfo = await _access.GetPrintInformation(serialNumber);

            return PrintDetailsResponse.Success(printInfo);
        }
    }
}
