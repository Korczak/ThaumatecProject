using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Thaumatec.Core.Configuration
{
    public class ConfigurationValidation : IStartupValidation
    {
        public bool Success { get; }
        public string DisplayName { get; } = "Configuration File";
        public IEnumerable<string> GetErrors()
        {
            foreach (string property in _missingProperties)
            {
                yield return $"missing property {property}";
            }
        }

        private readonly ImmutableList<string> _missingProperties;

        private ConfigurationValidation(bool success, IEnumerable<string> missingProperties)
        {
            Success = success;
            _missingProperties = missingProperties.ToImmutableList();
        }

        public static ConfigurationValidation CreateSuccess() => new ConfigurationValidation(true, ImmutableList<string>.Empty);
        public static ConfigurationValidation CreateError(IEnumerable<string> missingProperties) => new ConfigurationValidation(false, missingProperties);

    }
}
