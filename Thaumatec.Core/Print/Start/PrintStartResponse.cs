using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Print.Start
{
    public class PrintStartResponse
    {
        public bool IsSuccess { get; }

        private PrintStartResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static PrintStartResponse Failure() => new PrintStartResponse(false);
        public static PrintStartResponse Success() => new PrintStartResponse(true);
    }
}
