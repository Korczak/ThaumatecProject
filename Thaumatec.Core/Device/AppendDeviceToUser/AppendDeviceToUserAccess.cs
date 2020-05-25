using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Models.Device;
using Thaumatec.Core.Database.Models.UserDevice;
using Thaumatec.Core.Database.Settings;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Device.AppendDeviceToUser
{
    public class AppendDeviceToUserAccess
    {
        public async Task AppendDeviceToUser(ObjectId deviceId, ObjectId userId, string deviceName, string location)
        {
            using(var handler = new DatabaseHandler())
            {

                var userDeviceToInsert = new UserDevices()
                {
                    DeviceId = deviceId,
                    UserId = userId
                };
                try
                {
                    await handler.StartTransaction();
                    await handler.db.UserDevices.InsertOneAsync(userDeviceToInsert);
                    await handler.db.Devices.UpdateOneAsync(
                        x => x.Id == deviceId,
                        new UpdateDefinitionBuilder<Devices>()
                            .Set(x => x.Name, deviceName)
                            .Set(x => x.Location, location));

                    await handler.CommitTransaction();
                }
                catch
                {
                    await handler.AbortTransaction();
                }

                await handler.StartTransaction();
                try
                {
                    await handler.CommitTransaction();
                }
                catch
                {
                    await handler.AbortTransaction();
                }
            }
        }

        public async Task<ObjectId> DoesDeviceBelongsToUser(ObjectId userId, ObjectId deviceId)
        {
            using (var handler = new DatabaseHandler())
            {
                return await handler.db.UserDevices.AsQueryable()
                    .Where(x => x.DeviceId == deviceId && x.UserId == userId)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<ObjectId> GetUserId(string username)
        {
            using (var handler = new DatabaseHandler())
            {
                return await handler.db.Users.AsQueryable()
                    .Where(x => x.Name == username)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
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
