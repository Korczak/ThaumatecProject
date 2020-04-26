using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Models.User;
using Thaumatec.Core.Database.Settings;

namespace Thaumatec.Core.Users.Register
{
    public class UserRegisterAccess
    {
        public async Task RegisterUser(UserRegistered change)
        {
            using (var handler = new DatabaseHandler())
            {
                await handler.StartTransaction();

                try
                {
                    var user = new Database.Models.User.Users()
                    {
                        Name = change.Username,
                        Password = change.HashedPassword,
                        Role = Constants.Role.Admin
                    };

                    await handler.db.Users.InsertOneAsync(user);
                    await handler.CommitTransaction();
                }
                catch
                {
                    await handler.AbortTransaction();
                    throw;
                }
            }
        }
    }
}