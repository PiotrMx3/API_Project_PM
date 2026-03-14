using API_Project_PM.Models;

namespace API_Project_PM.Services
{
    public class InMemoryStockMovementRepository : IStockMovementRepository
    {
        private static readonly List<StockMovement> _movements = new()
        {
            new StockMovement { Id = 1, ArticleId = 1, LocationId = 1, Quantity = 100, MovementType = "IN", MovementDate = DateTime.UtcNow.AddDays(-2) },
            new StockMovement { Id = 2, ArticleId = 1, LocationId = 1, Quantity = 5, MovementType = "OUT", MovementDate = DateTime.UtcNow }
        };

        public Task<IEnumerable<StockMovement>> GetAll()
        {
            return Task.FromResult<IEnumerable<StockMovement>>(_movements);
        }

        public Task<StockMovement?> GetById(int id)
        {
            StockMovement? result = _movements.FirstOrDefault(m => m.Id == id);
            if (result is null) return Task.FromResult<StockMovement?>(null);

            return Task.FromResult<StockMovement?>(result);
        }
    }
}
