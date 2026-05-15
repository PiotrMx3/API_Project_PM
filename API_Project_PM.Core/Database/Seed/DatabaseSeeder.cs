using API_Project_PM.Core.Enums;
using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Database.Seed
{
    public static class DatabaseSeeder
    {
        public static void Seed(AppDBContext context)
        {
            var categories = new List<Category>
            {
                new() { Name = "Elektronica" },
                new() { Name = "Mechanisch" },
                new() { Name = "Verbruiksmateriaal" }
            };

            var locations = new List<Location>
            {
                new() { Zone = "A", Rack = "01", Shelf = "1", Box = "A", LocationType = LocationType.Stock },
                new() { Zone = "A", Rack = "01", Shelf = "2", Box = "B", LocationType = LocationType.Stock },
                new() { Zone = "B", Rack = "03", Shelf = "1", Box = "C", LocationType = LocationType.Overstock }
            };

            var suppliers = new List<Supplier>
            {
                new() { Name = "TechSupply BV", VatNumber = "NL123456789B01", ContactEmail = "info@techsupply.nl", Country = "Nederland", PaymentTerms = "30 dagen", TaxRate = 21, IsActive = true },
                new() { Name = "MechParts GmbH", VatNumber = "DE987654321", ContactEmail = "orders@mechparts.de", Country = "Duitsland", PaymentTerms = "14 dagen", TaxRate = 19, IsActive = true }
            };

            context.AddRange(categories);
            context.AddRange(locations);
            context.AddRange(suppliers);
            context.SaveChanges();

            var parts = new List<Part>
            {
                new() { Sku = "ELEC-001", Name = "Arduino Uno R3", Price = 24.99m, Unit = "stuk", IsSellItem = true, AddInfo = "Microcontroller board", CategoryId = categories[0].Id, DefaultLocationId = locations[0].Id },
                new() { Sku = "ELEC-002", Name = "Raspberry Pi 4B 4GB", Price = 69.95m, Unit = "stuk", IsSellItem = true, AddInfo = "Single-board computer", CategoryId = categories[0].Id, DefaultLocationId = locations[0].Id },
                new() { Sku = "MECH-001", Name = "M4 Bouten Set", Price = 5.50m, Unit = "set", IsSellItem = false, AddInfo = "100 stuks M4x10", CategoryId = categories[1].Id, DefaultLocationId = locations[1].Id },
                new() { Sku = "CONS-001", Name = "Soldeertin 1mm", Price = 8.75m, Unit = "rol", IsSellItem = false, AddInfo = "250g spoel, loodvrij", CategoryId = categories[2].Id, DefaultLocationId = null }
            };

            context.AddRange(parts);
            context.SaveChanges();

            var partSuppliers = new List<PartSupplier>
            {
                new() { PartId = parts[0].Id, SupplierId = suppliers[0].Id, SupplierPrice = 18.50m, IsPreferred = true },
                new() { PartId = parts[1].Id, SupplierId = suppliers[0].Id, SupplierPrice = 55.00m, IsPreferred = true },
                new() { PartId = parts[2].Id, SupplierId = suppliers[1].Id, SupplierPrice = 3.20m, IsPreferred = true },
                new() { PartId = parts[3].Id, SupplierId = suppliers[0].Id, SupplierPrice = 6.90m, IsPreferred = false }
            };

            var stockItems = new List<StockItem>
            {
                new() { PartId = parts[0].Id, LocationId = locations[0].Id, Quantity = 25 },
                new() { PartId = parts[1].Id, LocationId = locations[0].Id, Quantity = 10 },
                new() { PartId = parts[2].Id, LocationId = locations[1].Id, Quantity = 500 },
                new() { PartId = parts[3].Id, LocationId = locations[2].Id, Quantity = 15 }
            };

            context.AddRange(partSuppliers);
            context.AddRange(stockItems);
            context.SaveChanges();

            var stockMovements = new List<StockMovement>
            {
                new() { PartId = parts[0].Id, LocationId = locations[0].Id, Quantity = 25, MovementType = MovementType.In, MovementDate = DateTime.UtcNow.AddDays(-7), TransferGroupId = Guid.NewGuid() },
                new() { PartId = parts[1].Id, LocationId = locations[0].Id, Quantity = 10, MovementType = MovementType.In, MovementDate = DateTime.UtcNow.AddDays(-5), TransferGroupId = Guid.NewGuid() },
                new() { PartId = parts[2].Id, LocationId = locations[1].Id, Quantity = 500, MovementType = MovementType.In, MovementDate = DateTime.UtcNow.AddDays(-3), TransferGroupId = Guid.NewGuid() },
                new() { PartId = parts[3].Id, LocationId = locations[2].Id, Quantity = 15, MovementType = MovementType.In, MovementDate = DateTime.UtcNow.AddDays(-1), TransferGroupId = Guid.NewGuid() }
            };
           // Save for Id
            context.AddRange(stockMovements);
            context.SaveChanges();
        }
    }
}
