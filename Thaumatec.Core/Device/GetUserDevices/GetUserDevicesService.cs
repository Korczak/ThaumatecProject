using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace Thaumatec.Core.Device.GetUserDevices
{
    public class GetUserDevicesService
    {
        private readonly GetUserDevicesAccess _access;

        public GetUserDevicesService(GetUserDevicesAccess access)
        {
            _access = access;
        }

        public async Task<GetUserDevicesResponse> GetUserDevices(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            var userId = await _access.GetUserId(username);
            if (userId == default)
                return GetUserDevicesResponse.Failure();

            var devices = await _access.GetDevicesForUser(userId);
            return GetUserDevicesResponse.Successfull(devices);
        }
    }
}
