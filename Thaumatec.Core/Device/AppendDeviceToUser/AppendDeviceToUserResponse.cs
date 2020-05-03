using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Device.AppendDeviceToUser
{
    public class AppendDeviceToUserResponse
    {
        public AppendDeviceToUserResult Result { get; }

        private AppendDeviceToUserResponse(AppendDeviceToUserResult result)
        {
            Result = result;
        }

        public static AppendDeviceToUserResponse Success() => new AppendDeviceToUserResponse(AppendDeviceToUserResult.Success);
        public static AppendDeviceToUserResponse Failure(AppendDeviceToUserResult result) => new AppendDeviceToUserResponse(result);
    }
}
