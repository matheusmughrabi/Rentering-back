using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Contracts.Domain.ValueObjects
{
    public class IdentityRGValueObject : BaseValueObject
    {
        public IdentityRGValueObject(string identityRG)
        {
            IdentityRG = identityRG;
        }

        public string IdentityRG { get; private set; }

        public override string ToString()
        {
            return IdentityRG;
        }
    }
}
