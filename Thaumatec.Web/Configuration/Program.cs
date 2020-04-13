using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Thaumatec.Web.Configuration;

namespace Thaumatec.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(Serilog.Events.LogEventLevel.Error)
                .WriteTo.Console()
                .CreateLogger();
            try {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Startup error");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .RunWindowServiceIfWindows()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static IHostBuilder RunWindowServiceIfWindows(this IHostBuilder host)
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                host.UseWindowsService()
                    .ConfigureServices(services =>
                        services.AddHostedService<Worker>());
            }

            return host;
        }
    }
}
