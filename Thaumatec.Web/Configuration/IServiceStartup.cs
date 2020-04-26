using Microsoft.Extensions.DependencyInjection;

namespace Thaumatec.Web.Configuration
{
    public interface IServiceStartup
    {
        void ConfigureServices(IServiceCollection services);
    }
}
