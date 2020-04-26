using Thaumatec.Core.Users.Constants;

namespace Thaumatec.Core.Users.Login
{
    public class UserLoginFoundUser
    {
        public string Id { get; }
        public string Username { get; }
        public string HashedPassword { get; }
        public Role Role { get; }

        public UserLoginFoundUser(string id, string username, string hashedPassword, Role role)
        {
            Id = id;
            Username = username;
            HashedPassword = hashedPassword;
            Role = role;
        }
    }
}