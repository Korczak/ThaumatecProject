using Microsoft.AspNetCore.Builder;

namespace Thaumatec.Web.Configuration
{
    public interface IControllerStartup
    {
        void ConfigureController(IApplicationBuilder app);
    }
}
