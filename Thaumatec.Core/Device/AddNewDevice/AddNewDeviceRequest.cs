namespace Thaumatec.Core.Device.AddNewDevice
{
    public class AddNewDeviceRequest
    {
        public string Name { get; }
        public string Location { get; }
        public string SerialNumber { get; }

        public AddNewDeviceRequest(string name, string location, string serialNumber)
        {
            Name = name;
            Location = location;
            SerialNumber = serialNumber;
        }
    }
}
