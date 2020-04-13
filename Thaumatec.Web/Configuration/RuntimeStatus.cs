using Thaumatec.Core.Configuration;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Thaumatec.Web.Configuration
{
    public class RuntimeStatus
    {
        public bool IsGood => AllValidations.All(v => v.Success);
        public IEnumerable<IStartupValidation> AllValidations => _allValidations;

        private ImmutableList<IStartupValidation> _allValidations;

        public void Update(params IStartupValidation[] validations)
        {
            _allValidations = validations.ToImmutableList();
        }
    }
}
