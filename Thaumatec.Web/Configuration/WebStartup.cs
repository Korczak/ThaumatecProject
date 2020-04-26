using Thaumatec.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using System.Linq;
using Thaumatec.Core.Logging;

namespace Thaumatec.Web.Configuration
{
    public class WebStartup : IStartupStep
    {
        const string DEVELOPMENT_CLIENT_URL = "http://localhost:8080";
        private readonly IApplicationBuilder _app;
        private readonly IWebHostEnvironment _env;

        public WebStartup(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _app = app;
            _env = env;
        }

        public IStartupValidation Configure()
        {
            if (_env.IsDevelopment())
            {
                _app.UseDeveloperExceptionPage();
            }

            _app.UseOpenApi();
            _app.UseSwaggerUi3();


            _app.UseMiddleware<ErrorLoggingMiddleware>();
            _app.UseRouting();
            _app.UseAuthentication();
            _app.UseMvc();

            _app.MapWhen(
                x => !x.Request.Path.Value.StartsWith("/swagger"),
                configuration =>
                {
                    configuration.UseSpa(spa =>
                    {
                        spa.Options.SourcePath = "Client";
                        if (_env.IsDevelopment())
                        {
                            spa.UseProxyToSpaDevelopmentServer(DEVELOPMENT_CLIENT_URL);
                        }
                    });
                }
            );


            var address = _app.ServerFeatures.Get<IServerAddressesFeature>()?.Addresses?.FirstOrDefault();

            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings();
                settings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
                settings.Converters.Add(new StringEnumConverter());
                return settings;
            };

            return new WebStartupValidation();
        }
    }
}
