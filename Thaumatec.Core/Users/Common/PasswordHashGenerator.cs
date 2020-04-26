using Sodium;

namespace Thaumatec.Core.Users.Common
{
    public class PasswordHashGenerator
    {
        public string HashPassword(string password)
        {
            return PasswordHash
                .ArgonHashString(password)
                .Replace("\0", "");
        }

    }
}
