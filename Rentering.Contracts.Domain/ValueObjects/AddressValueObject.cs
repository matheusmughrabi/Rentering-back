using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Contracts.Domain.ValueObjects
{
    public class AddressValueObject : BaseValueObject
    {
        public AddressValueObject(string street, string bairro, string cidade, string cep, string estado)
        {
            Street = street;
            Bairro = bairro;
            Cidade = cidade;
            CEP = cep;
            Estado = estado;
        }

        public string Street { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string CEP { get; private set; }
        public string Estado { get; private set; }

        public override string ToString()
        {
            return $"{Street} - {Bairro} - {Cidade} - {CEP} - {Estado}";
        }
    }
}
