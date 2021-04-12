using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Contracts.Domain.ValueObjects
{
    public class CPFValueObject : BaseValueObject
    {
        public CPFValueObject(string cpf)
        {
            CPF = cpf;
        }

        public string CPF { get; private set; }

        public override string ToString()
        {
            return CPF;
        }
    }
}
