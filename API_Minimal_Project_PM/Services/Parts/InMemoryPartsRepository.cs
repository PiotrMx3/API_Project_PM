using API_Minimal_Project_PM.Models;

namespace API_Minimal_Project_PM.Services.Parts
{
    public class InMemoryPartsRepository : IPartsRepository
    {
        private static readonly List<Part> _parts = new()
        {
            new Part
            {
                Id = 1001,
                Sku = "SENS-TEMP-01",
                Name = "Temperature Sensor PT100",
                Price = 45.50m,
                Unit = "stuks",
                SupplierId = 1,
                LocationId = 1,
                CategoryId = 1,
                isSellItem = true,
                AddInfo = "Fragile, handle with care"
            },
            new Part
            {
                Id = 1002,
                Sku = "MET-BRACKET-M4",
                Name = "Steel Bracket M4",
                Price = 2.15m,
                Unit = "stuks",
                SupplierId = 2,
                LocationId = 3,
                CategoryId = 2,
                isSellItem = false,
                AddInfo = "Heavy box"
            },
            new Part
            {
                Id = 1003,
                Sku = "ROB-ARM-AXIS1",
                Name = "Robotic Arm Axis Motor",
                Price = 850.00m,
                Unit = "stuks",
                SupplierId = 3,
                LocationId = 4,
                CategoryId = 1,
                isSellItem = true,
                AddInfo = "Includes warranty card"
            },
            new Part
            {
                Id = 1004,
                Sku = "ELEC-CABLE-3G2-5mm2",
                Name = "Copper Wire H07RN-F 3G2.5mm",
                Price = 15.99m,
                SupplierId = 4,
                LocationId = 2,
                Unit = "m",
                CategoryId = 1,
                isSellItem = true,
                AddInfo = "Standard power cable"
            }
        };
        public Task CreatePart(Part item)
        {
            if(_parts.Any(p => p.Sku.Equals(item.Sku, StringComparison.OrdinalIgnoreCase))) {

                throw new InvalidOperationException("Deze onderdeel bestaat al");
            }

            var id = _parts.LastOrDefault()?.Id ?? 0;

            item.Id = id + 1;

            _parts.Add(item);

            return Task.CompletedTask;
        }

        public Task<bool> DeletePart(int id)
        {
            Part? existing = _parts.FirstOrDefault(i => i.Id == id);
            if (existing is null) return Task.FromResult(false);

            _parts.Remove(existing);

            return Task.FromResult(true);
        }
        public Task<bool> UpdatePart(int id, Part item)
        {
            Part? existing = _parts.FirstOrDefault(i => i.Id == id);
            if (existing is null) return Task.FromResult(false);

            existing.Name = item.Name;
            existing.Price = item.Price;
            existing.Unit = item.Unit;
            existing.SupplierId = item.SupplierId;
            existing.LocationId = item.LocationId;
            existing.CategoryId = item.CategoryId;
            existing.isSellItem = item.isSellItem;
            existing.AddInfo = item.AddInfo;

            return Task.FromResult(true);
        }

        public Task<IEnumerable<Part>> GetAllParts()
        {
            return Task.FromResult(_parts.AsEnumerable());
        }

        public Task<Part?> GetPartById(int id)
        {
            Part? result = _parts.FirstOrDefault(e => e.Id == id);

            if (result is null) return Task.FromResult<Part?>(null);


            return Task.FromResult<Part?>(result);
        }

    }
}
