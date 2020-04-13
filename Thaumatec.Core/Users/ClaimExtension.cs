using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Thaumatec.Core.Users
{
    public static class ClaimExtensions
    {
        public static void SetClaim(this IPrincipal currentPrincipal, string key, string value)
        {
            var identity = currentPrincipal?.Identity as ClaimsIdentity;
            if (identity == null) return;
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
            {
                identity.RemoveClaim(existingClaim);
            }
            identity.AddClaim(new Claim(key, value));
        }

        public static string GetClaim(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal?.Identity as ClaimsIdentity;
            if (identity == null) return null;
            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }

        public static void RemoveClaim(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal?.Identity as ClaimsIdentity;
            if (identity == null) return;

            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
            {
                identity.RemoveClaim(existingClaim);
            }
        }

        //To get information about user data, replace this function in future
        public static BasicUserData GetBasicUserData(this IPrincipal currentPrincipal)
        {
            var result = new BasicUserData(1, "test");
            return result;
        }
    }

}
