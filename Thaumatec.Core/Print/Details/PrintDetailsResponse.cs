using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Print.Details
{
    public class PrintDetailsResponse
    {
        public bool IsSuccess { get; }
        public PrintInformation PrintInformation { get; }

        private PrintDetailsResponse(bool isSuccess, PrintInformation printInformation)
        {
            IsSuccess = isSuccess;
            PrintInformation = printInformation;
        }

        public static PrintDetailsResponse Failure() => new PrintDetailsResponse(false, null);
        public static PrintDetailsResponse Success(PrintInformation print) => new PrintDetailsResponse(true, print);
    }
}
