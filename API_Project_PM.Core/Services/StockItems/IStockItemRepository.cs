using API_Project_PM.Core.DTOs.StockItems;
using API_Project_PM.Core.Enums;
using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.StockItems
{
    public interface IStockItemRepository
    {
        Task<IEnumerable<StockItem>> GetAllAsync();
        Task<bool> UpsertAsync(int partId, int locationId, int quantity, MovementType type);
        Task<bool> DeleteAsync(int partId, int locationId);
        Task<IEnumerable<StockItem>> GetByPartIdAsync(int partId);
        Task<IEnumerable<StockItem>> GetByLocationIdAsync(int locationId);

    }
}
