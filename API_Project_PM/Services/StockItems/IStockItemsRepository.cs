using API_Project_PM.Models;

namespace API_Project_PM.Services.StockItems
{
    public interface IStockItemsRepository
    {
        Task<IEnumerable<StockItem>> GetAll();
        Task<StockItem?> GetById(int id);
    }
}
