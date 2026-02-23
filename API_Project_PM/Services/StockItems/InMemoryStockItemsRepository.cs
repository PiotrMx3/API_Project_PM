using API_Project_PM.Models;

namespace API_Project_PM.Services.StockItems
{
    public class InMemoryStockItemsRepository : IStockItemsRepository
    {

        private static readonly List<StockItem> _stockItems = new()
        {
            // FUSE-10A (PartId = 1)
            new StockItem { Id = 1, PartId = 1, LocationId = 1, Quantity = 30 },  // Picking
            new StockItem { Id = 2, PartId = 1, LocationId = 4, Quantity = 120 }, // Overflow

            // FUSE-16A (PartId = 2)
            new StockItem { Id = 3, PartId = 2, LocationId = 2, Quantity = 25 },

            // RELAY-230V (PartId = 3)
            new StockItem { Id = 4, PartId = 3, LocationId = 3, Quantity = 15 },
            new StockItem { Id = 5, PartId = 3, LocationId = 5, Quantity = 60 },  // Overflow

            // CONTACTOR-24V (PartId = 4)
            new StockItem { Id = 6, PartId = 4, LocationId = 1, Quantity = 8 },

            // MCB-C16 (PartId = 5)
            new StockItem { Id = 7, PartId = 5, LocationId = 2, Quantity = 40 }
        };

        public Task<IEnumerable<StockItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<StockItem?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
