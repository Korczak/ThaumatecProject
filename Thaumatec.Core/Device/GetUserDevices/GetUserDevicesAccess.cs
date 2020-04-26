using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Models.Device;
using Thaumatec.Core.Database.Settings;

namespace Thaumatec.Core.Device.GetUserDevices
{
    public class GetUserDevicesAccess
    {
        public async Task<IEnumerable<GetUserDeviceItem>> GetDevicesForUser(ObjectId userId)
        {
            using(var handler = new DatabaseHandler())
            {
                var devicesQuery = from userDevices in handler.db.UserDevices.AsQueryable()
                                   where userDevices.UserId == userId
                                   join devices in handler.db.Devices.AsQueryable() on userDevices.DeviceId equals devices.Id
                                   select new GetUserDeviceItem(devices.Name, devices.LastUpdateDateTime, devices.LastPrintDateTime, devices.Status);

                return await devicesQuery.ToListAsync();
            }
        }
    }
}
