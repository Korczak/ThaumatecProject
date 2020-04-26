using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Thaumatec.Core.Users.Common;
using Thaumatec.Core.Users.Constants;
using Thaumatec.Core.Users.Login;
using Thaumatec.Core.Users.Register;
using Thaumatec.Web.Configuration;

namespace Thaumatec.Web.Users
{
    public class UsersStartup : IServiceStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PasswordHashGenerator>();
            services.AddSingleton<RandomPasswordGenerator>();
            services.AddSingleton<UserRegisterService>();
            services.AddSingleton<UserRegisterAccess>();
            services.AddSingleton<UserLoginService>();
            services.AddSingleton<UserLoginDataAccess>();

            services.AddAuthentication(
                    CookieAuthenticationDefaults.AuthenticationScheme
                )
                .AddCookie(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.Events.OnRedirectToAccessDenied = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        };

                        options.Events.OnRedirectToLogin = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        };

                        options.LoginPath = "/login";
                        options.LogoutPath = "/logout";
                        options.SlidingExpiration = true;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);


                    }
                );

            services.AddAuthorization(o =>
            {
                o.AddPolicy("DefaultPolicy", b =>
                {
                    b.RequireAuthenticatedUser();
                });

                o.AddPolicy(PolicyCollection.Administrator, policy => policy.RequireRole(Role.Admin.ToString()));
                o.AddPolicy(PolicyCollection.User, policy => policy.RequireRole(Role.User.ToString(),
                                                                                Role.Admin.ToString()));
            });
        }
    }
}
