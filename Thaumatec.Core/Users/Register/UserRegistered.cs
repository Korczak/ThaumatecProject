
using Thaumatec.Core.Users.Constants;

namespace Thaumatec.Core.Users.Register
{
    public class UserRegistered
    {
        public string Username { get; }
        public string HashedPassword { get; }
        public Role Role { get; }
        public BasicUserData RegisteringUser { get; }

        public UserRegistered(string username, string hashedPassword, Role role, BasicUserData registeringUser)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Role = role;
            RegisteringUser = registeringUser;
        }
    }
}