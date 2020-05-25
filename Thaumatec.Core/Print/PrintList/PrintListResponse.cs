using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Print.PrintList
{
    public class PrintListResponse
    {
        public bool IsSuccess { get; }
        public List<PrintInformation> PrintInformation { get; }

        private PrintListResponse(bool isSuccess, List<PrintInformation> printInformation)
        {
            IsSuccess = isSuccess;
            PrintInformation = printInformation;
        }

        public static PrintListResponse Failure() => new PrintListResponse(false, null);
        public static PrintListResponse Success(List<PrintInformation> prints) => new PrintListResponse(true, prints);
    }
}
