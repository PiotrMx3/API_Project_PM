using API_Project_PM.Core.Models;
namespace API_Project_PM.Core.Services.Suppliers
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


        public Task CreateSupplier(Supplier item)
        {
            var id = _suppliers.LastOrDefault()?.Id ?? 0;

            item.Id = id + 1;

            _suppliers.Add(item);

            return Task.CompletedTask;
        }

        public Task<bool> DeleteSupplier(int id)
        {
            Supplier? existing = _suppliers.FirstOrDefault(i => i.Id == id);
            if (existing is null) return Task.FromResult(false);

            _suppliers.Remove(existing);
            return Task.FromResult(true);
        }
        public Task<bool> UpdateSupplier(int id, Supplier item)
        {
            Supplier? existing = _suppliers.FirstOrDefault(i => i.Id == id);
            if (existing is null) return Task.FromResult(false);

            existing.Name = item.Name;
            existing.VatNumber = item.VatNumber;
            existing.ContactEmail = item.ContactEmail;
            existing.Country = item.Country;
            existing.PaymentTerms = item.PaymentTerms;
            existing.TaxRate = item.TaxRate;
            existing.IsActive = item.IsActive;

            return Task.FromResult(true);
        }

        public Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            return Task.FromResult(_suppliers.AsEnumerable());
        }

        public Task<Supplier?> GetSupplierById(int id)
        {
            Supplier? result = _suppliers.FirstOrDefault(e => e.Id == id);

            if (result is null) return Task.FromResult<Supplier?>(null);

            return Task.FromResult<Supplier?>(result);

        }

    }
}
