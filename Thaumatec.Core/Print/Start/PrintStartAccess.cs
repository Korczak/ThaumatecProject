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

namespace Thaumatec.Core.Print.Start
{
    public class PrintStartAccess
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

        public async Task StartNewPrint(string serialNumber, string Name, string gcode)
        {
            using (var handler = new DatabaseHandler())
            {
                try
                {
                    await handler.StartTransaction();

                    var printId = ObjectId.GenerateNewId();

                    var printToAdd = new Prints
                    {
                        Id = printId,
                        Name = Name,
                        SerialNumber = serialNumber,
                        Status = PrintStatus.Printing,
                        StartedTime = LocalDateTime.FromDateTime(DateTime.Now),
                        Gcode = gcode
                    };
                    await handler.db.Prints.InsertOneAsync(printToAdd);

                    await handler.db.Devices
                        .UpdateOneAsync(x =>
                            x.SerialNumber == serialNumber,
                            new UpdateDefinitionBuilder<Devices>()
                                .Set(x => x.Status, DeviceStatus.Printing)
                                .Push(x => x.PrintsIds, printId));
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
