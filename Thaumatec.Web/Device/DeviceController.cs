using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Thaumatec.Core.Device.AddNewDevice;
using Thaumatec.Core.Device.AppendDeviceToUser;
using Thaumatec.Core.Device.GetUserDevices;

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

        [HttpGet("/api/devices/{id}")]
        [Produces(typeof(GetUserDevicesResponse))]
        public async Task<IActionResult> GetDevicesForUser(string id)
        {
            var response = await _getUserDevicesService.GetUserDevices(new ObjectId(id));

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
            var input = new AppendDeviceToUserInput(request.SerialNumber, new ObjectId("5ea468bb8fb72137e41523f9"));
            await _appendDeviceToUserService.AppendDevice(input);

            return Ok();
        }
    }
}