using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Print.PrintList
{
    public class PrintInformation
    {
        public string Name { get; }
        public LocalDateTime StartedTime { get; }
        public LocalDateTime StoppedTime { get; }
        public PrintStatus Status { get; }

        public PrintInformation(string name, LocalDateTime startedTime, LocalDateTime stoppedTime, PrintStatus status)
        {
            Name = name;
            StartedTime = startedTime;
            StoppedTime = stoppedTime;
            Status = status;
        }
    }
}
