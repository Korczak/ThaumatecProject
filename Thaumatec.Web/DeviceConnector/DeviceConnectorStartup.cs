using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet.Client.Options;
using Thaumatec.Core.DeviceConnector.ActualStatus;
using Thaumatec.Core.DeviceConnector.Initialize;
using Thaumatec.Web.Configuration;

namespace Thaumatec.Web.DeviceConnector
{
    public class DeviceConnectorStartup : IServiceStartup, IControllerStartup
    {
        public void ConfigureController(IApplicationBuilder app)
        {
            var mqttOptions = (IMqttClientOptions)app.ApplicationServices.GetService(typeof(IMqttClientOptions));
            var actualStatusService = (DeviceConnectorActualStatusService)app.ApplicationServices.GetService(typeof(DeviceConnectorActualStatusService));
            var initService = (DeviceConnectorInitializeService)app.ApplicationServices.GetService(typeof(DeviceConnectorInitializeService)); 
            
            _ = new DeviceConnectorController(mqttOptions, initService, actualStatusService);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DeviceConnectorActualStatusService>();
            services.AddSingleton<DeviceConnectorActualStatusAccess>();

            services.AddSingleton<DeviceConnectorInitializeService>();
            services.AddSingleton<DeviceConnectorInitializeAccess>();
        }
    }
}
