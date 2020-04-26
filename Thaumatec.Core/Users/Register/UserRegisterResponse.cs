namespace Thaumatec.Core.Users.Register
{
    public class UserRegisterResponse
    {
        public UserRegisterStatus Status { get; }
        public string Username { get; }
        public string TemporaryPassword { get; }

        public UserRegisterResponse(UserRegisterStatus status, string username, string temporaryPassword)
        {
            Status = status;
            Username = username;
            TemporaryPassword = temporaryPassword;
        }
    }
}