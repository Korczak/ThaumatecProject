namespace Thaumatec.Core.DeviceConnector.PrintStatus
{
    public class PrintStatusUpdated
    {
        public double TemperatureBed { get; }
        public double TemperatureTool0 { get; }
        public double TemperatureTool1 { get; }

        public PrintStatusUpdated(double? temperatureBed, double? temperatureTool0, double? temperatureTool1)
        {
            TemperatureBed = temperatureBed ?? 0;
            TemperatureTool0 = temperatureTool0 ?? 0;
            TemperatureTool1 = temperatureTool1 ?? 0;
        }
    }
}