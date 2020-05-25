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

namespace Thaumatec.Core.DeviceConnector.PrintAbort
{
    public class DeviceConnectorPrintAbortAccess
    {
        public async Task EndPrint(string serialNumber, DeviceConnectorPrintAbortRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            using (var handler = new DatabaseHandler())
            {
                try
                {
                    await handler.StartTransaction();
                    await handler.db.Prints.UpdateOneAsync(
                        x => x.SerialNumber == serialNumber && x.Status == Device.Constants.PrintStatus.Printing,
                        new UpdateDefinitionBuilder<Prints>()
                            .Set(x => x.Status, Device.Constants.PrintStatus.Aborted));
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
