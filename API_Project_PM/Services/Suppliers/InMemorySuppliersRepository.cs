using API_Project_PM.Models;

namespace API_Project_PM.Services.Suppliers
{
    public class InMemorySuppliersRepository : ISuppliersRepository
    {
        private static readonly List<Supplier> _suppliers = new()
        {
            new Supplier
            {
                Id = 1,
                Name = "DutchTech Supplies Ltd.",
                VatNumber = "NL123456789B01",
                ContactEmail = "sales@dutchtech.nl",
                Country = "Netherlands",
                PaymentTerms = "30 days invoice",
                TaxRate = 21.0m
            },
            new Supplier
            {
                Id = 2,
                Name = "Flemish Metalworks Ltd.",
                VatNumber = "BE0987654321",
                ContactEmail = "info@metalworks.be",
                Country = "Belgium",
                PaymentTerms = "14 days invoice",
                TaxRate = 21.0m
            },
            new Supplier
            {
                Id = 3,
                Name = "Amsterdam Robotics",
                VatNumber = "NL987654321B02",
                ContactEmail = "b2b@amsterdamrobotics.nl",
                Country = "Netherlands",
                PaymentTerms = "Prepayment",
                TaxRate = 21.0m
            },
            new Supplier
            {
                Id = 4,
                Name = "Brussels Electronics Ltd.",
                VatNumber = "BE0123456789",
                ContactEmail = "contact@brusselselectronics.be",
                Country = "Belgium",
                PaymentTerms = "60 days invoice",
                TaxRate = 21.0m
            }
        };

        public Task<IEnumerable<Supplier>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Supplier?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
