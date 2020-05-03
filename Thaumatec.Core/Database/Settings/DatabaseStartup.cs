using Thaumatec.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Thaumatec.Core.Database.Settings
{
    public class DatabaseStartup : IStartupStep
    {
        private readonly Config _config;

        public DatabaseStartup(Config config)
        {
            _config = config;
        }

        public IStartupValidation Configure()
        {
            DatabaseConnection.SetConnection(_config);

            using(var handler = new DatabaseHandler())
            {
                bool isMongoLive = handler.db.Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
                if(isMongoLive)
                    return DatabaseValidation.Successfull();
                return DatabaseValidation.Failure();
            }
        }
    }
}
