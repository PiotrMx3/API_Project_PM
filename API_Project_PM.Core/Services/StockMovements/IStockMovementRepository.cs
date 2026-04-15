using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.StockMovements
{
    public interface IStockMovementRepository
    {
        Task<IEnumerable<StockMovement>> GetAllStockMovements();
        Task<StockMovement?> GetStockMovementById(int id);
        Task CreateStockMovement(StockMovement item);


    }
}
