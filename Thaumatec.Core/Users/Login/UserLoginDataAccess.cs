using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Thaumatec.Core.Database.Settings;

namespace Thaumatec.Core.Users.Login
{
    public class UserLoginDataAccess
    {
        public async Task<UserLoginFoundUser> FindUser(string username)
        {
            using (var handler = new DatabaseHandler())
            {
                var foundUser = await handler.db.Users.AsQueryable()
                    .Where(u => u.Name == username)
                    .Select(u => new UserLoginFoundUser(u.Id.ToString(), u.Name, u.Password, Constants.Role.Admin))
                    .FirstOrDefaultAsync();

                return foundUser;
            }
        }
    }
}