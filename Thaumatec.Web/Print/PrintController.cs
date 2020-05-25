using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thaumatec.Core.Print.Details;
using Thaumatec.Core.Print.PrintList;
using Thaumatec.Core.Print.Start;
using Thaumatec.Core.Print.Stop;
using Thaumatec.Core.Users;
using Thaumatec.Core.Users.UserRole;

namespace Thaumatec.Web.Print
{
    public class PrintController : ControllerBase
    {
        private readonly PrintDetailsService _printDetailsService;
        private readonly PrintStartService _printStartService; 
        private readonly PrintStopService _printStopService;
        private readonly PrintListService _printListService;

        public PrintController(PrintDetailsService printDetailsService, PrintStartService printStartService, PrintStopService printStopService, PrintListService printListService)
        {
            _printDetailsService = printDetailsService;
            _printStartService = printStartService;
            _printStopService = printStopService;
            _printListService = printListService;
        }

        [HttpGet("/api/print/{serialNumber}")]
        [Produces(typeof(PrintDetailsResponse))]
        public async Task<IActionResult> GetActivePrintForDevice(string serialNumber)
        {
            var response = await _printDetailsService.GetPrintDetails(serialNumber);

            return Ok(response);
        }

        [HttpPost("/api/print/start")]
        [Produces(typeof(PrintStartResponse))]
        public async Task<IActionResult> StartPrint([FromBody] PrintStartRequest request)
        {
            var response = await _printStartService.StartPrint(request);

            return Ok(response);
        }

        [HttpPost("/api/print/stop")]
        [Produces(typeof(PrintStartResponse))]
        public async Task<IActionResult> StopPrint([FromBody] PrintStopRequest request)
        {
            var response = await _printStopService.StopPrint(request);

            return Ok(response);
        }

        [HttpPost("/api/print/list")]
        [Produces(typeof(PrintListResponse))]
        public async Task<IActionResult> GetPrints()
        {
            var username = User.GetClaim(CustomClaimTypes.Username);

            var response = await _printListService.GetPrintList(username);

            return Ok(response);
        }
    }
}