using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Core.Services.Parts
{
    public class PartService : IPartRepository
    {
        private readonly AppDBContext _db;

        public PartService(AppDBContext db)
        {
            this._db = db;
        }
        public async Task<Part> CreateAsync(Part item)
        {
            _db.Parts.Add(item);

            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Part? existing = await _db.Parts.FindAsync(id);

            if (existing is null) return false;

            bool hasStockQuantity = await _db.StockItems.AnyAsync(s => s.PartId == id && s.Quantity > 0);

            if(hasStockQuantity) throw new InvalidOperationException("Onderdeel heeft nog voorraad");

            bool hasSupplier = await _db.PartSuppliers.AnyAsync(p => p.PartId == id);

            if (hasSupplier) throw new InvalidOperationException("Onderdeel heeft nog Leverancier");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<Part>> GetAllAsync()
        {
            return await _db.Parts.Include(p => p.Category)
                .Include(p => p.DefaultLocation)
                .ToListAsync();
        }

        public async Task<Part?> GetByIdAsync(int id)
        {
            return await _db.Parts
                .Include(p => p.Category)
                .Include(p => p.DefaultLocation)
                .Include(p => p.PartSuppliers)
                .ThenInclude(ps => ps.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<bool> UpdateAsync(Part item)
        {
            Part? toBeUptdate = await _db.Parts.FindAsync(item.Id);

            if (toBeUptdate is null) return false;

            item.Sku = toBeUptdate.Sku;

            _db.Entry(toBeUptdate).CurrentValues.SetValues(item);

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
