using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NodaTime;

namespace Thaumatec.Core.Database.Models.Device
{
    public class Temperatures
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.Double)]
        public double TemperatureBed { get; set; }
        [BsonRepresentation(BsonType.Double)]
        public double TemperatureTool0 { get; set; }
        [BsonRepresentation(BsonType.Double)]
        public double TemperatureTool1 { get; set; }
        public LocalDateTime DateTime { get; set; }
    }
}
