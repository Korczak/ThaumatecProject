using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mongo.Migration.Documents;
using Mongo.Migration.Documents.Attributes;

namespace Thaumatec.Core.Database.Models
{
    [CollectionLocation("Pallets", "Pallets")]
    [StartUpVersion("0.1.0")]
    [RuntimeVersion("0.2.0")]
    public class Pallets : IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string RFID { get; set; }
        public DocumentVersion Version { get; set; }

    }
}
