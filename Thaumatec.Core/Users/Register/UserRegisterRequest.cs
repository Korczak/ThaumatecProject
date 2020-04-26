
using Thaumatec.Core.Users.Constants;

namespace Thaumatec.Core.Users.Register
{
    public class UserRegisterRequest
    {
        public string Username { get; }
        public string Password { get; }

        public UserRegisterRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}