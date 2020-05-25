using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Settings;

namespace Thaumatec.Core.Device.GetDetails
{
    public class DeviceGetDetailsAccess
    {
        public async Task<DeviceGetDetailsResponse> GetDetails(string serialNumber)
        {
            using (var handler = new DatabaseHandler())
            {
                return await handler.db.Devices.AsQueryable().Where(x => x.SerialNumber == serialNumber)
                    .Select(x => new DeviceGetDetailsResponse(x.Name, x.LastPrintDateTime, x.Location, x.PrintsIds.Count(), x.Status))
                    .FirstOrDefaultAsync();
            }
        }
    }
}
