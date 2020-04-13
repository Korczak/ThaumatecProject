using Thaumatec.Core.Pallet.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mongo.Migration.Documents;
using Mongo.Migration.Documents.Attributes;

namespace Thaumatec.Core.Database.Models
{
    [CollectionLocation("Pallets", "Pallets")]
    public class Pallets : IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string RFID { get; set; }
        public PalletStatus Status { get; set; }
        public DocumentVersion Version { get; set; }

    }
}
