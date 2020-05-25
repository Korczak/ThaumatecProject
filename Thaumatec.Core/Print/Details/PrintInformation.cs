using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using Thaumatec.Core.Device.Constants;

namespace Thaumatec.Core.Print.Details
{
    public class PrintInformation
    {
        public string Name { get; }
        public LocalDateTime StartedTime { get; }
        public IEnumerable<TemperatureItem> Temperatures { get; }

        public PrintInformation(string name, LocalDateTime startedTime, List<TemperatureItem> temperatures)
        {
            Name = name;
            StartedTime = startedTime;
            Temperatures = temperatures;
        }
    }
}
