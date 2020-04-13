using Thaumatec.Core.Database.Settings;
using Thaumatec.DatabaseTest.Configuration;
using System;
using Xunit;
using Thaumatec.Core.Database.Models;

namespace Thaumatec.DatabaseTest
{
    [Collection("Database")]
    public sealed class PalletTests : IDisposable
    {
        public PalletTests()
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
                await handler.db.PalletsCollection.InsertOneAsync(new Pallets() { RFID = "1" });
            }
        }
    }
}
