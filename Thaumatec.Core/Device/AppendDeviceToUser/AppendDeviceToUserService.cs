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

        public Task AppendDevice(AppendDeviceToUserInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            return InternalAppendDevice(input);
        }

        private async Task InternalAppendDevice(AppendDeviceToUserInput input)
        {
            var deviceId = await _access.GetDeviceId(input.SerialNumber);
            if (deviceId == default)
                return;

            await _access.AppendDeviceToUser(deviceId, input.UserId);
        }
    }
}
