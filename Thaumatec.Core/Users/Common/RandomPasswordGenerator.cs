using Sodium;
using System;


namespace Thaumatec.Core.Users.Common
{
    public class RandomPasswordGenerator
    {
        public string GenerateRandomPassword(int length)
        {
            var randomPassword = Convert
                    .ToBase64String(SodiumCore.GetRandomBytes(length))
                    .Replace("+", "")
                    .Replace("/", "")
                    .Replace("=", "")
                    .Substring(0, length);

            return randomPassword;
        }
    }
}
