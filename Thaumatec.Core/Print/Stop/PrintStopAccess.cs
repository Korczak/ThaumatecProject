using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Models.Device;
using Thaumatec.Core.Database.Settings;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Print.Stop
{
    public class PrintStopAccess
    {
        public async Task<DeviceStatus> GetDeviceStatus(string serialNumber)
        {
            using(var handler = new DatabaseHandler())
            {
                return await handler.db.Devices.AsQueryable()
                    .Where(x => x.SerialNumber == serialNumber)
                    .Select(x => x.Status)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task StopPrint(string serialNumber, string printName)
        {
            using (var handler = new DatabaseHandler())
            {
                try
                {
                    await handler.StartTransaction();
                    await handler.db.Prints
                        .UpdateOneAsync(x =>
                            x.SerialNumber == serialNumber && x.Name == printName,
                            new UpdateDefinitionBuilder<Prints>()
                                .Set(x => x.Status, PrintStatus.Aborted));
                    await handler.db.Devices
                        .UpdateOneAsync(x =>x.SerialNumber == serialNumber,
                            new UpdateDefinitionBuilder<Devices>()
                                .Set(x => x.Status, DeviceStatus.Aborting));
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
