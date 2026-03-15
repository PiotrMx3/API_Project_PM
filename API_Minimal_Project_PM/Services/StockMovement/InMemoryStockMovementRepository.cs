using API_Minimal_Project_PM.Models;

namespace API_Minimal_Project_PM.Services.StockMovements
{
    public class InMemoryStockMovementRepository : IStockMovementRepository
    {
        private static readonly List<StockMovement> _stockMovements = new()
        {
            new StockMovement { Id = 1, ArticleId = 1, LocationId = 1, Quantity = 100, MovementType = "IN", MovementDate = DateTime.UtcNow.AddDays(-2) },
            new StockMovement { Id = 2, ArticleId = 1, LocationId = 1, Quantity = 5, MovementType = "OUT", MovementDate = DateTime.UtcNow }
        };

        public Task CreateStockMovement(StockMovement item)
        {
            var id = _stockMovements.LastOrDefault()?.Id ?? 0;

            item.Id = id + 1;

            _stockMovements.Add(item);

            return Task.CompletedTask;
        }


        public Task<IEnumerable<StockMovement>> GetAllStockMovements()
        {
            return Task.FromResult<IEnumerable<StockMovement>>(_stockMovements);
        }

        public Task<StockMovement?> GetStockMovementById(int id)
        {
            StockMovement? result = _stockMovements.FirstOrDefault(m => m.Id == id);
            if (result is null) return Task.FromResult<StockMovement?>(null);

            return Task.FromResult<StockMovement?>(result);
        }

    }
}
