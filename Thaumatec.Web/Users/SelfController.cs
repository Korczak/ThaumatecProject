
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Thaumatec.Core.Users;
using Thaumatec.Core.Users.Constants;
using Thaumatec.Core.Users.Login;
using Thaumatec.Core.Users.UserRole;
using Thaumatec.Web.Configuration;

namespace Thaumatec.Web.Users
{
    [Authorize]
    public class SelfController : ControllerBase
    {
        private readonly UserLoginService _userLoginService;

        public SelfController(UserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        /// <summary>
        /// Login using a username and password.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("api/self/login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserLoginResult))]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var result = await _userLoginService.Login(request);

            if (result.Result == UserLoginResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(CustomClaimTypes.Username, request.Username),
                    new Claim(ClaimTypes.Role, result.Role.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                Thread.CurrentPrincipal = principal;
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [AllowOnFirstLogin]
        [HttpPost("api/self/logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync();
        }

        /// <summary>
        /// Check the current user's authentication status.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("api/self")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserLoginResponse))]
        public UserLoginResponse GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.GetClaim(CustomClaimTypes.Username);
                string roleString = User.GetClaim(ClaimTypes.Role);
                string firstLoginString = User.GetClaim(CustomClaimTypes.FirstLogin);

                bool.TryParse(firstLoginString, out bool firstLogin);

                Enum.TryParse<Role>(roleString, out var role);

                return UserLoginResponse.Successfull("0", username, role);
            }
            else
            {
                return UserLoginResponse.PasswordOrUsernameError();
            }
        }
    }
}
