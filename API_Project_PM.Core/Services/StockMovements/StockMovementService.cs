using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Core.Services.StockMovements
{
    public class StockMovementService : IStockMovementRepository
    {
        private readonly AppDBContext _db;

        public StockMovementService(AppDBContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<StockMovement>> GetAllStockMovements()
        {
            return await _db.StockMovements
                .Include(sm => sm.Part)
                .Include(sm => sm.Location)
                .ToListAsync();
        }

        public async Task<StockMovement?> GetStockMovementById(int id)
        {
            return await _db.StockMovements
                .Include(sm => sm.Part)
                .Include(sm => sm.Location)
                .FirstOrDefaultAsync(sm => sm.Id == id);
        }

        public async Task CreateStockMovement(StockMovement item)
        {
            _db.StockMovements.Add(item);
            await _db.SaveChangesAsync();
        }
    }
}
