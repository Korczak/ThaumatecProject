using Thaumatec.Core.Database.Settings;
using Thaumatec.DatabaseTest.Configuration;
using System;
using Xunit;
using Thaumatec.Core.Database.Models.Device;

namespace Thaumatec.DatabaseTest
{
    [Collection("Database")]
    public sealed class DeviceTests : IDisposable
    {
        public DeviceTests()
        {
            DatabaseSetup.Setup();
        }

        public void Dispose()
        {
            DatabaseSetup.Cleanup();
        }

        [Fact(DisplayName = "Test")]
        public async void Test()
        {
            using(var handler = new DatabaseHandler())
            {
                await handler.db.Devices.InsertOneAsync(new Devices() { Name = "Nowe urzadzenie testowe" });
            }
        }
    }
}
