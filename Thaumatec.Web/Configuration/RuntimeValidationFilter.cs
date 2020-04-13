using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Thaumatec.Web.Configuration
{
    public class RuntimeValidationFilter : IAsyncActionFilter
    {
        private readonly RuntimeStatus _runtimeStatus;

        public RuntimeValidationFilter(RuntimeStatus runtimeStatus)
        {
            _runtimeStatus = runtimeStatus;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(_runtimeStatus.IsGood)
            {
                await next();
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ContentResult() { Content = GetErrorText() };
            }
        }

        private string GetErrorText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("System status:");
            sb.AppendLine();

            foreach(var validation in _runtimeStatus.AllValidations)
            {
                if(validation.Success)
                {
                    sb.AppendLine($"{validation.DisplayName} - OK");
                }
                else
                {
                    sb.AppendLine($"{validation.DisplayName} - ERROR");
                    foreach(var error in validation.GetErrors())
                    {
                        sb.AppendLine($"\t{error}");
                    }
                }
            }
            return sb.ToString();
        }
    }
}
