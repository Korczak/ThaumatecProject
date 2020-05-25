using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Settings;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Print.Details
{
    public class PrintDetailsAccess
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

        public async Task<PrintInformation> GetPrintInformation(string serialNumber)
        {
            using (var handler = new DatabaseHandler())
            {
                var temperatures = await handler.db.Prints.AsQueryable()
                    .Where(x => x.SerialNumber == serialNumber && x.Status == PrintStatus.Printing)
                    .SelectMany(x => x.Temperatures)
                    .Select(x => new TemperatureItem(x.DateTime, x.TemperatureBed, x.TemperatureTool0, x.TemperatureTool1))
                    .ToListAsync();


                var printInformation = await handler.db.Prints.AsQueryable()
                    .Where(x => x.SerialNumber == serialNumber && x.Status == PrintStatus.Printing)
                    .Select(x => new
                    {
                        Name = x.Name,
                        StartedTime = x.StartedTime
                    })
                    .FirstOrDefaultAsync();
                return new PrintInformation(printInformation.Name, printInformation.StartedTime, temperatures);
            }
        }
    }
}
