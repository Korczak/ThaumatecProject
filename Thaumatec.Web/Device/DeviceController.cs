using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Thaumatec.Core.Device.AddNewDevice;
using Thaumatec.Core.Device.AppendDeviceToUser;
using Thaumatec.Core.Device.GetUserDevices;
using Thaumatec.Core.Users;
using Thaumatec.Core.Users.UserRole;

namespace Thaumatec.Web.Device
{
    public class DeviceController : ControllerBase
    {
        private readonly GetUserDevicesService _getUserDevicesService;
        private readonly AddNewDeviceService _addNewDeviceService;
        private readonly AppendDeviceToUserService _appendDeviceToUserService;

        public DeviceController(
            GetUserDevicesService getUserDevicesService,
            AddNewDeviceService addNewDeviceService,
            AppendDeviceToUserService appendDeviceToUserService)
        {
            _getUserDevicesService = getUserDevicesService;
            _addNewDeviceService = addNewDeviceService;
            _appendDeviceToUserService = appendDeviceToUserService;
        }

        [HttpGet("/api/user/devices/")]
        [Produces(typeof(GetUserDevicesResponse))]
        public async Task<IActionResult> GetDevicesForUser()
        {
            var username = User.GetClaim(CustomClaimTypes.Username);

            var response = await _getUserDevicesService.GetUserDevices(username);

            return Ok(response);
        }

        [HttpPost("/api/devices/")]
        public async Task<IActionResult> AddNewDevice(AddNewDeviceRequest request)
        {
            await _addNewDeviceService.AddNewDeice(request);

            return Ok();
        }

        [HttpPost("/api/device_connector/append_device")]
        public async Task<IActionResult> AppendDevice(AppendDeviceToUserRequest request)
        {
            var username = User.GetClaim(CustomClaimTypes.Username);

            var input = new AppendDeviceToUserInput(request.SerialNumber, username);
            await _appendDeviceToUserService.AppendDevice(input);

            return Ok();
        }
    }
}