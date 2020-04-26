using MongoDB.Bson;
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

        public async Task<GetUserDevicesResponse> GetUserDevices(ObjectId userId)
        {
            var devices = await _access.GetDevicesForUser(userId);
            return new GetUserDevicesResponse(devices);
        }
    }
}
