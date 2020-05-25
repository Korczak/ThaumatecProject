using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Print.Stop
{
    public class PrintStopResponse
    {
        public bool IsSuccess { get; }

        private PrintStopResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static PrintStopResponse Failure() => new PrintStopResponse(false);
        public static PrintStopResponse Success() => new PrintStopResponse(true);
    }
}
