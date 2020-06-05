﻿using Thaumatec.Core.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDb.Bson.NodaTime;

namespace Thaumatec.Core.Database.Settings
{
    public static class DatabaseConnection
    {
        public static IMongoDatabase db { get => _db;  }
        public static MongoClient Client { get => _client; }

        private static IMongoDatabase _db;
        private static MongoClient _client;
        private static bool isConnectionEstablished = false;
        static DatabaseConnection()
        {
            //To store enum as strings not ints https://stackoverflow.com/a/18874502
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);

            //NodaTime Serializer
            NodaTimeSerializers.Register();
        }

        public static void SetConnection(Config config)
        {
            SetConnection(config.ConnectionString, config.DatabaseName);
        }

        public static void SetConnection(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _db = Client.GetDatabase(databaseName);
        }
    }
}
