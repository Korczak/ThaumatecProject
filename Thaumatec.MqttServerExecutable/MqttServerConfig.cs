using Serilog.Events;
using System.Collections.Generic;

namespace Thaumatec.MqttServerExecutable
{
    public class MqttServerConfig
    {
        public LogEventLevel LogLevel { get; set; }
        public int Port { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
