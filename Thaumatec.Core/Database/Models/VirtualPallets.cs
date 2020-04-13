using Mongo.Migration.Documents;
using Mongo.Migration.Documents.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Thaumatec.Core.Database.Models
{
    [CollectionLocation("VirtualPallets", "VirtualPallets")]
    public class VirtualPallets : IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<ProgramStepsHistories> ProgramStepsHistories {get;set; }
        public DocumentVersion Version { get; set; }

    }
}
