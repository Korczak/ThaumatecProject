using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Models.Device;
using Thaumatec.Core.Database.Settings;

namespace Thaumatec.Core.DeviceConnector.Initialize
{
    public class DeviceConnectorInitializeAccess
    {
        public async Task<ObjectId> GetDeviceWithSerialNumber(string serialNumber)
        {
            using(var handler = new DatabaseHandler())
            {
                return await handler.db.Devices.AsQueryable().Where(x => x.SerialNumber == serialNumber).Select(x => x.Id).FirstOrDefaultAsync();
            }
        }

        public async Task RegisterNewDevice(string serialNumber)
        {
            using (var handler = new DatabaseHandler())
            {
                try
                {
                    await handler.StartTransaction();
                    var deviceToAdd = new Devices
                    {
                        SerialNumber = serialNumber
                    };

                    await handler.db.Devices.InsertOneAsync(deviceToAdd);
                    await handler.CommitTransaction();
                }
                catch
                {
                    await handler.AbortTransaction();
                }
            }
        }
    }
}
