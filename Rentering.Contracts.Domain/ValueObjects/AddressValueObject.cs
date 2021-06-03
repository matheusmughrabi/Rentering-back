using Rentering.Common.Shared.ValueObjects;
using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Domain.ValueObjects
{
    public class AddressValueObject : BaseValueObject
    {
        protected AddressValueObject()
        {
        }

        public AddressValueObject(string street, string neighborhood, string city, string cep, e_BrazilStates state)
        {
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            CEP = cep;
            State = state;
        }

        public string Street { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string CEP { get; private set; }
        public e_BrazilStates State { get; private set; }

        public override string ToString()
        {
            return $"{Street} - {Neighborhood} - {City} - {CEP} - {State}";
        }
    }
}
