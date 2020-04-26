
using Thaumatec.Core.Users.Constants;

namespace Thaumatec.Core.Users.Register
{
    public class UserRegistered
    {
        public string Username { get; }
        public string HashedPassword { get; }
        public BasicUserData RegisteringUser { get; }

        public UserRegistered(string username, string hashedPassword, BasicUserData registeringUser)
        {
            Username = username;
            HashedPassword = hashedPassword;
            RegisteringUser = registeringUser;
        }
    }
}