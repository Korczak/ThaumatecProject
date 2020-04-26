using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Models.UserDevice;
using Thaumatec.Core.Database.Settings;

namespace Thaumatec.Core.Device.AppendDeviceToUser
{
    public class AppendDeviceToUserAccess
    {
        public async Task AppendDeviceToUser(ObjectId deviceId, ObjectId userId)
        {
            using(var handler = new DatabaseHandler())
            {
                var userDeviceToInsert = new UserDevices()
                {
                    DeviceId = deviceId,
                    UserId = userId
                };

                await handler.StartTransaction();
                try
                {
                    await handler.db.UserDevices.InsertOneAsync(userDeviceToInsert);
                    await handler.CommitTransaction();
                }
                catch
                {
                    await handler.AbortTransaction();
                }
            }
        }

        public async Task<ObjectId> GetDeviceId(string serialNumber)
        {
            using (var handler = new DatabaseHandler())
            {
                return await handler.db.Devices.AsQueryable()
                    .Where(x => x.SerialNumber == serialNumber)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
            }
        }
    }
}
