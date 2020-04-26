using MongoDB.Driver;

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
        public IMongoCollection<Models.Device.Devices> Devices => _db.GetCollection<Models.Device.Devices>("Device");
        public IMongoCollection<Models.User.Users> Users => _db.GetCollection<Models.User.Users>("User");
        public IMongoCollection<Models.UserDevice.UserDevices> UserDevices => _db.GetCollection<Models.UserDevice.UserDevices>("UserDevices");

    }
}
