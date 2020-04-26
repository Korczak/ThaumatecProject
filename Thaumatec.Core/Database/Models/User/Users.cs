using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Thaumatec.Core.Database.Models.UserDevice;
using Thaumatec.Core.Users.Constants;

namespace Thaumatec.Core.Database.Models.User
{
    public class Users
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
