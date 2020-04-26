using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NodaTime;
using System.Collections.Generic;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Database.Models.Device
{
    public class Devices
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public LocalDateTime LastUpdateDateTime { get; set; }
        public LocalDateTime LastPrintDateTime { get; set; }
        public string Location { get; set; }
        public DeviceStatus Status { get; set; }
        public IEnumerable<ObjectId> PrintsIds { get; set; } = new List<ObjectId>();
    }
}
