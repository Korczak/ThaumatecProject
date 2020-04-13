using System;
using System.Collections.Generic;
using Thaumatec.Core.Configuration;

namespace Thaumatec.MqttServer
{
    public class MqttServerValidation : IStartupValidation
    {
        public string DisplayName => "MQTT Server Service";
        public bool Success => true;

        public IEnumerable<string> GetErrors() => Array.Empty<string>();

    }
}
