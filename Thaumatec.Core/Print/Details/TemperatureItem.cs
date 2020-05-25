using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Print.Details
{
    public class TemperatureItem
    {
        public LocalDateTime DateTime { get; }
        public double TemperatureBed { get; }
        public double TemperatureTool0 { get; }
        public double TemperatureTool1 { get; }

        public TemperatureItem(LocalDateTime dateTime, double temperatureBed, double temperatureTool0, double temperatureTool1)
        {
            DateTime = dateTime;
            TemperatureBed = temperatureBed;
            TemperatureTool0 = temperatureTool0;
            TemperatureTool1 = temperatureTool1;
        }
    }
}
