namespace API_Project_PM.Models
{
    public interface IStockMovementRepository
    {
        Task<IEnumerable<StockMovement>> GetAll();
        Task<StockMovement?> GetById(int id);
    }
}
