namespace API_Project_PM.Models
{
    public interface IStockMovementRepository
    {
        Task<IEnumerable<StockMovement>> GetAllStockMovements();
        Task<StockMovement?> GetStockMovementById(int id);
        Task CreateStockMovement(StockMovement item);

    }
}
