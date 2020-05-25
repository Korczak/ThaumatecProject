using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Thaumatec.Core.Device.AppendDeviceToUser
{
    public class AppendDeviceToUserService
    {
        private readonly AppendDeviceToUserAccess _access;

        public AppendDeviceToUserService(AppendDeviceToUserAccess access)
        {
            _access = access;
        }

        public Task<AppendDeviceToUserResponse> AppendDevice(AppendDeviceToUserInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            return InternalAppendDevice(input);
        }

        private async Task<AppendDeviceToUserResponse> InternalAppendDevice(AppendDeviceToUserInput input)
        {
            var deviceId = await _access.GetDeviceId(input.SerialNumber);
            var userId = await _access.GetUserId(input.Username);

            if (deviceId == default)
                return AppendDeviceToUserResponse.Failure(AppendDeviceToUserResult.DeviceNotExist);
            if (userId == default)
                return AppendDeviceToUserResponse.Failure(AppendDeviceToUserResult.UserNotExist);

            var usersDeviceId = await _access.DoesDeviceBelongsToUser(userId, deviceId);
            if (usersDeviceId != default)
                return AppendDeviceToUserResponse.Failure(AppendDeviceToUserResult.UserAlreadyAddedThisDevice);

            await _access.AppendDeviceToUser(deviceId, userId, input.Name, input.Location);
            return AppendDeviceToUserResponse.Success();
        }
    }
}
