

using API_Minimal_Project_PM.Models;

namespace API_Minimal_Project_PM.Services.StockMovements
{
    public interface IStockMovementRepository
    {
        Task<IEnumerable<StockMovement>> GetAllStockMovements();
        Task<StockMovement?> GetStockMovementById(int id);
        Task CreateStockMovement(StockMovement item);


    }
}
