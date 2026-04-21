using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace API_Project_PM.Core.Services.Locations
{
    public class LocationService : ILocationRepository
    {
        private readonly AppDBContext _db;

        public LocationService(AppDBContext db)
        {
            this._db = db;
        }

        public async Task<Location> CreateAsync(Location item)
        {
            _db.Locations.Add(item);

            await _db.SaveChangesAsync();

            return item;

        }

        public async Task<bool> DeleteAsync(int id)
        {

            // 1 query
            Location? result = await _db.Locations.FindAsync(id);
            if (result is null) return false;

            // check active stock in StockItem
            bool hasStock = await _db.StockItems.AnyAsync(s => s.LocationId == result.Id && s.Quantity > 0);

            if (hasStock) throw new InvalidOperationException("Locatie heeft nog voorraad");

            // Two separate DB operations require atomic behavior
            // DefaultLocationId null-update may succeed while soft delete fails,
            // leaving data in inconsistent state transaction prevents this

            // 2 query

            // Set Null Parts.DefaultLocation
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                await _db.Parts
                    .Where(p => p.DefaultLocationId == result.Id)
                    .ExecuteUpdateAsync(s => s.SetProperty(p => p.DefaultLocationId, (int?)null));

                result.IsDeleted = true;
                result.DeletedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _db.Locations.ToListAsync();
        }


        public async Task<Location?> GetByIdAsync(int id)
        {
            return await _db.Locations.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Location item)
        {
            var toUpdate = await _db.Locations.FindAsync(item.Id);
            if (toUpdate is null) return false;

            // TODO: Ensure that using a random ID doesn't bring a deleted location back to life.
            _db.Entry(toUpdate).CurrentValues.SetValues(item);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
