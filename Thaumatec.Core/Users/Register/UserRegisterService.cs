using System.Threading.Tasks;
using Thaumatec.Core.Users.Common;

namespace Thaumatec.Core.Users.Register
{
    public class UserRegisterService
    {
        private readonly UserRegisterAccess _access;
        private readonly PasswordHashGenerator _hashGenerator;
        private readonly RandomPasswordGenerator _passwordGenerator;

        public UserRegisterService(UserRegisterAccess access, PasswordHashGenerator hashGenerator, RandomPasswordGenerator passwordGenerator)
        {
            _access = access;
            _hashGenerator = hashGenerator;
            _passwordGenerator = passwordGenerator;
        }

        public async Task<UserRegisterResponse> RegisterUser(UserRegisterRequest input, BasicUserData registeringUser)
        {
            var hashedPassword = _hashGenerator.HashPassword(input.Password);

            var change = new UserRegistered(input.Username, hashedPassword, registeringUser);

            await _access.RegisterUser(change);
            return new UserRegisterResponse(UserRegisterStatus.Success, input.Username, input.Password);
        }
    }
}
