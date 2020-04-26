using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Models.Device;
using Thaumatec.Core.Database.Settings;

namespace Thaumatec.Core.Device.AddNewDevice
{
    public class AddNewDeviceAccess
    {

        public async Task AddNewDevice(AddNewDeviceRequest request)
        {
            using(var handler = new DatabaseHandler())
            {
                await handler.StartTransaction();
                try
                {
                    var deviceToInsert = new Devices()
                    {
                        Name = request.Name,
                        Location = request.Location,
                        SerialNumber = request.SerialNumber
                    };
                    await handler.db.Devices.InsertOneAsync(deviceToInsert);
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
