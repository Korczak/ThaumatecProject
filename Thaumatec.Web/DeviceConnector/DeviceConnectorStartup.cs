using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet.Client.Options;
using Thaumatec.Core.DeviceConnector.ActualStatus;
using Thaumatec.Core.DeviceConnector.Initialize;
using Thaumatec.Core.DeviceConnector.PrintAbort;
using Thaumatec.Core.DeviceConnector.PrintEnd;
using Thaumatec.Core.DeviceConnector.PrintStatus;
using Thaumatec.Core.Mqtt;
using Thaumatec.Web.Configuration;

namespace Thaumatec.Web.DeviceConnector
{
    public class DeviceConnectorStartup : IServiceStartup, IControllerStartup
    {
        public void ConfigureController(IApplicationBuilder app)
        {
            var mqttClientSettings = (MqttClientSettings)app.ApplicationServices.GetService(typeof(MqttClientSettings));
            var actualStatusService = (DeviceConnectorActualStatusService)app.ApplicationServices.GetService(typeof(DeviceConnectorActualStatusService));
            var initService = (DeviceConnectorInitializeService)app.ApplicationServices.GetService(typeof(DeviceConnectorInitializeService)); 
            var endAccess = (DeviceConnectorPrintEndAccess)app.ApplicationServices.GetService(typeof(DeviceConnectorPrintEndAccess)); 
            var abortAccess = (DeviceConnectorPrintAbortAccess)app.ApplicationServices.GetService(typeof(DeviceConnectorPrintAbortAccess)); 
            var statusService = (DeviceConnectorPrintStatusService)app.ApplicationServices.GetService(typeof(DeviceConnectorPrintStatusService)); 
            
            _ = new DeviceConnectorController(mqttClientSettings, initService, actualStatusService, statusService, endAccess, abortAccess);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DeviceConnectorActualStatusService>();
            services.AddSingleton<DeviceConnectorActualStatusAccess>();

            services.AddSingleton<DeviceConnectorInitializeService>();
            services.AddSingleton<DeviceConnectorInitializeAccess>();

            services.AddSingleton<DeviceConnectorPrintStatusService>();
            services.AddSingleton<DeviceConnectorPrintStatusAccess>();

            services.AddSingleton<DeviceConnectorPrintEndAccess>();
            
            services.AddSingleton<DeviceConnectorPrintAbortAccess>();
        }
    }
}
