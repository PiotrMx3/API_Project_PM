using API_Project_PM.Models;

namespace API_Project_PM.Services.Parts
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
                Quantity = 150,
                SupplierId = 1,
                LocationId = 1,
                isSellItem = true,
                AddInfo = "Fragile, handle with care"
            },
            new Part
            {
                Id = 1002,
                Sku = "MET-BRACKET-M4",
                Name = "Steel Bracket M4",
                Price = 2.15m,
                Quantity = 5000,
                SupplierId = 2,
                LocationId = 3,
                isSellItem = false,
                AddInfo = "Heavy box"
            },
            new Part
            {
                Id = 1003,
                Sku = "ROB-ARM-AXIS1",
                Name = "Robotic Arm Axis Motor",
                Price = 850.00m,
                Quantity = 12,
                SupplierId = 3,
                LocationId = 4,
                isSellItem = true,
                AddInfo = "Includes warranty card"
            },
            new Part
            {
                Id = 1004,
                Sku = "ELEC-CABLE-10M",
                Name = "Copper Wire 10m",
                Price = 15.99m,
                Quantity = 320,
                SupplierId = 4,
                LocationId = 2,
                isSellItem = true,
                AddInfo = "Standard power cable"
            }
        };
        public Task<IEnumerable<Part>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Part?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
