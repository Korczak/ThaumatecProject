using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Models.Device;
using Thaumatec.Core.Database.Settings;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.DeviceConnector.PrintStatus
{
    public class DeviceConnectorPrintStatusAccess
    {
        public async Task UpdatePrintStatus(string serialNumber, PrintStatusUpdated updated)
        {
            using(var handler = new DatabaseHandler())
            {
                try
                {
                    await handler.StartTransaction();

                    var temperatureToAdd = new Temperatures()
                    {
                        TemperatureBed = updated.TemperatureBed,
                        TemperatureTool0 = updated.TemperatureTool0,
                        TemperatureTool1 = updated.TemperatureTool1
                    };

                    await handler.db.Prints.UpdateOneAsync(
                        x => x.SerialNumber == serialNumber && x.Status == Device.Constants.PrintStatus.Printing,
                        new UpdateDefinitionBuilder<Prints>().Push(x => x.Temperatures, temperatureToAdd));
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
