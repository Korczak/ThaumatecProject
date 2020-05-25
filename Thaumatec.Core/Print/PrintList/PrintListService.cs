using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Thaumatec.Core.Print.PrintList
{
    public class PrintListService
    {
        private readonly PrintListAccess _access;

        public PrintListService(PrintListAccess access)
        {
            _access = access;
        }

        public async Task<PrintListResponse> GetPrintList(string username)
        {
            var serialNumbers = await _access.GetUsersDevices(username);

            var prints = await _access.GetPrintsInformation(serialNumbers);

            return PrintListResponse.Success(prints);
        }
    }
}
