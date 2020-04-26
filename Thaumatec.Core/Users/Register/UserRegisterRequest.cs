
using Thaumatec.Core.Users.Constants;

namespace Thaumatec.Core.Users.Register
{
    public class UserRegisterRequest
    {
        public string Username { get; }
        public string Password { get; }
        public Role Role { get; }

        public UserRegisterRequest(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
    }
}