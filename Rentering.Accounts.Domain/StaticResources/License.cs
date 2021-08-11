using System.Collections.Generic;

namespace Rentering.Accounts.Domain.StaticResources
{
    public static class Licenses
    {
        private static License freeLicense = new License(1, "Gratuita", "Plano gratuito com recursos básico", 0M);
        private static License standardLicense = new License(2, "Padrão", "Plano padrão com recursos um pouco melhores", 99.99M);
        private static License proLicense = new License(3, "Pro", "Plano Pro com todos os recursos", 199.99M);

        public static List<License> LicensesList = new List<License>()
        {
            freeLicense, 
            standardLicense, 
            proLicense
        };
    }

    public class License
    {
        public License(int code, string name, string description, decimal price)
        {
            Code = code;
            Name = name;
            Description = description;
            Price = price;
        }

        public int Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
    }
}
