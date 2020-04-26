using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NodaTime;
using System.Collections.Generic;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Database.Models.Device
{
    public class Prints
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("serial_number")]
        public string SerialNumber { get; set; }
        [BsonElement("started_time")]
        public LocalDateTime StartedTime { get; set; }
        [BsonElement("stopped_time")]
        public LocalDateTime StoppedTime { get; set; }
        [BsonElement("temperatures")]
        public IEnumerable<Temperatures> Temperatures { get; set; }
        [BsonElement("status")]
        public PrintStatus Status { get; set; }
        [BsonElement("g_code")]
        public string Gcode { get; set; }
    }
}
