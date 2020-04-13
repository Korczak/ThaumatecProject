using Mongo.Migration.Migrations;
using Mongo.Migration.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Thaumatec.Core.Database.Models;

namespace Thaumatec.MongoMigrations.Scripts
{
    public class M001_Init : Migration<Pallets>
    {
        private readonly IVersionService _service;

        public M001_Init(IVersionService service) : base("0.1.0")
        {
            _service = service;
        }

        public override void Down(BsonDocument document)
        {
            var doors = document["Doors"].ToInt32();
            document.Add("Dors", doors);
            document.Remove("Doors");
        }

        public override void Up(BsonDocument document)
        {
            var doors = document["Dors"].ToInt32();
            document.Add("Doors", doors);
            document.Remove("Dors");
        }
    }
}
