using System;
using System.Collections.Generic;
using Thaumatec.Core.Configuration;

namespace Thaumatec.Core.Mqtt
{
    public class MqttClientValidation : IStartupValidation
    {
        public string DisplayName => "MQTT Client Service";
        public bool Success => true;

        public IEnumerable<string> GetErrors() => Array.Empty<string>();

    }
}
