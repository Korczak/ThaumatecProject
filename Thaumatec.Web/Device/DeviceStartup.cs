using Microsoft.Extensions.DependencyInjection;
using Thaumatec.Core.Device.AddNewDevice;
using Thaumatec.Core.Device.AppendDeviceToUser;
using Thaumatec.Core.Device.GetUserDevices;
using Thaumatec.Web.Configuration;

namespace Thaumatec.Web.Device
{
    public class DeviceStartup : IServiceStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AddNewDeviceService>();
            services.AddSingleton<AddNewDeviceAccess>();

            services.AddSingleton<GetUserDevicesService>();
            services.AddSingleton<GetUserDevicesAccess>();

            services.AddSingleton<AppendDeviceToUserService>();
            services.AddSingleton<AppendDeviceToUserAccess>();
        }
    }
}
