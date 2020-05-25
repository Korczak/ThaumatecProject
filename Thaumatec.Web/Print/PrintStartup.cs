using Microsoft.Extensions.DependencyInjection;
using Thaumatec.Core.Print.Details;
using Thaumatec.Core.Print.PrintList;
using Thaumatec.Core.Print.Start;
using Thaumatec.Core.Print.Stop;
using Thaumatec.Web.Configuration;

namespace Thaumatec.Web.Print
{
    public class PrintStartup : IServiceStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PrintDetailsService>();
            services.AddSingleton<PrintDetailsAccess>();

            services.AddSingleton<PrintStartService>();
            services.AddSingleton<PrintStartAccess>();

            services.AddSingleton<PrintStopService>();
            services.AddSingleton<PrintStopAccess>();

            services.AddSingleton<PrintListService>();
            services.AddSingleton<PrintListAccess>();
        }
    }
}
