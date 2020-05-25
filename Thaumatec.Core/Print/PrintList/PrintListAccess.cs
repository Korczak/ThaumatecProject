using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Settings;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Print.PrintList
{
    public class PrintListAccess
    {
        public async Task<List<string>> GetUsersDevices(string username)
        {
            using (var handler = new DatabaseHandler())
            {
                var devicesQuery = from users in handler.db.Users.AsQueryable()
                                   where users.Name == username
                                   join usersDevices in handler.db.UserDevices.AsQueryable() on users.Id equals usersDevices.UserId
                                   join devices in handler.db.Devices.AsQueryable() on usersDevices.DeviceId equals devices.Id
                                   select devices.SerialNumber;


                return await devicesQuery.ToListAsync();
            }

        }

        public async Task<List<PrintInformation>> GetPrintsInformation(IEnumerable<string> serialNumber)
        {
            using (var handler = new DatabaseHandler())
            {
                return await handler.db.Prints.AsQueryable()
                    .Where(x => serialNumber.Contains(x.SerialNumber) && x.Status == PrintStatus.Printing)
                    .Select(x => new PrintInformation(
                        x.Name, 
                        x.StartedTime, 
                        x.StoppedTime,
                        x.Status
                    ))
                    .ToListAsync();
            }
        }
    }
}
