
using Thaumatec.Core.Users.Constants;

namespace Thaumatec.Core.Users.Login
{
    public class UserLoginResponse
    {
        public UserLoginResult Result { get; }
        public string UserId { get; }
        public string Username { get; }
        public Role Role { get; }

        private UserLoginResponse(UserLoginResult result, string userId = default, string username = default, Role role = default)
        {
            Result = result;
            Role = role;
        }

        public static UserLoginResponse Successfull(string userId, string username, Role role) => new UserLoginResponse(UserLoginResult.Success, userId, username, role);
        public static UserLoginResponse PasswordOrUsernameError() => new UserLoginResponse(UserLoginResult.PasswordOrUsernameError);
    }
}