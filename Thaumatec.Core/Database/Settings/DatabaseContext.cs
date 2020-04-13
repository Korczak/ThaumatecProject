using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Thaumatec.Core.Database.Models;

namespace Thaumatec.Core.Database.Settings
{
    public class DatabaseContext
    {
        private readonly IMongoDatabase _db;
        private readonly MongoClient _client;
        public DatabaseContext(IMongoDatabase db, MongoClient client)
        {
            _db = db;
            _client = client;
        }

        public MongoClient Client { get => _client; }
        public IMongoCollection<Models.Pallets> PalletsCollection => _db.GetCollection<Models.Pallets>("Pallet");
        public IMongoQueryable<Models.Pallets> Pallets => _db.GetCollection<Models.Pallets>("Pallet").AsQueryable();
        public IMongoQueryable<VirtualPallets> VirtualPallets => _db.GetCollection<VirtualPallets>("VirtualPallet").AsQueryable();

    }
}
