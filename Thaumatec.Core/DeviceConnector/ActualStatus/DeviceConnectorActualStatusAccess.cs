using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Models.Device;
using Thaumatec.Core.Database.Settings;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.DeviceConnector.ActualStatus
{
    public class DeviceConnectorActualStatusAccess
    {
        public async Task UpdateDeviceStatus(DeviceStatus status, string serialNumber)
        {
            using(var handler = new DatabaseHandler())
            {
                try
                {
                    await handler.StartTransaction();
                    await handler.db.Devices.UpdateOneAsync(x => x.SerialNumber == serialNumber, new UpdateDefinitionBuilder<Devices>().Set(x => x.Status, status));
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
