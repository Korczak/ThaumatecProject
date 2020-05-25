using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Thaumatec.Core.Device.AddNewDevice;
using Thaumatec.Core.Device.AppendDeviceToUser;
using Thaumatec.Core.Device.GetDetails;
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
        private readonly DeviceGetDetailsAccess _deviceGetDetailsAccess;

        public DeviceController(
            GetUserDevicesService getUserDevicesService,
            AddNewDeviceService addNewDeviceService,
            AppendDeviceToUserService appendDeviceToUserService,
            DeviceGetDetailsAccess deviceGetDetailsAccess)
        {
            _getUserDevicesService = getUserDevicesService;
            _addNewDeviceService = addNewDeviceService;
            _appendDeviceToUserService = appendDeviceToUserService;
            _deviceGetDetailsAccess = deviceGetDetailsAccess;
        }

        [HttpGet("/api/user/devices")]
        [Produces(typeof(GetUserDevicesResponse))]
        public async Task<IActionResult> GetDevicesForUser()
        {
            var username = User.GetClaim(CustomClaimTypes.Username);

            var response = await _getUserDevicesService.GetUserDevices(username);

            return Ok(response);
        }


        [HttpGet("/api/device/{serialNumber}")]
        [Produces(typeof(DeviceGetDetailsResponse))]
        public async Task<IActionResult> GetDevice(string serialNumber)
        {
            var response = await _deviceGetDetailsAccess.GetDetails(serialNumber);

            return Ok(response);
        }

        [HttpPost("/api/devices")]
        public async Task<IActionResult> AddNewDevice([FromBody] AddNewDeviceRequest request)
        {
            await _addNewDeviceService.AddNewDevice(request);

            return Ok();
        }

        [HttpPost("/api/device_connector/append_device")]
        [Produces(typeof(AppendDeviceToUserResponse))]
        public async Task<IActionResult> AppendDevice([FromBody] AppendDeviceToUserRequest request)
        {
            var username = User.GetClaim(CustomClaimTypes.Username);

            var input = new AppendDeviceToUserInput(request.SerialNumber, username, request.Name, request.Location);
            var response = await _appendDeviceToUserService.AppendDevice(input);

            return Ok(response);
        }
    }
}