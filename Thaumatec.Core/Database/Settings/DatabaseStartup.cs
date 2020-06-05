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
            if (isConnectionEstablished())
            {
                return DatabaseValidation.Successfull();
            }
            return DatabaseValidation.Failure();
        }

        private bool isConnectionEstablished(int numOfTries = 30)
        {
            for (int i = 0; i < numOfTries; i++)
            {
                DatabaseConnection.SetConnection(_config);

                using (var handler = new DatabaseHandler())
                {
                    bool isMongoLive = handler.db.Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
                    if (isMongoLive)
                        return true;
                }
            }
            return false;
        }
    }
}
