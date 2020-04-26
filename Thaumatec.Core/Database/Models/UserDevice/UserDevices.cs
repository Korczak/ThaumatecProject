using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using Thaumatec.Core.Database.Models.Device;
using Thaumatec.Core.Database.Models.User;

namespace Thaumatec.Core.Database.Models.UserDevice
{
    public class UserDevices
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId DeviceId { get; set; }
    }
}
