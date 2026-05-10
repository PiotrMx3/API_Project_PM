using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.CustomExceptions;
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
            Part? existingSku = await _db.Parts.IgnoreQueryFilters().Where(p => p.Sku == item.Sku).FirstOrDefaultAsync();

            if(existingSku is not null && existingSku.IsDeleted) throw new ConflictException($"Deze Sku is inactive ID: {existingSku.Id}");
            if (existingSku is not null) throw new ConflictException($"Onderdeel met Sku {existingSku.Sku} bestaat al!");


            _ = await _db.Categories.FindAsync(item.CategoryId) ?? throw new NotFoundException($"Categorie met ID: {item.CategoryId} bestaat niet");

            if (item.DefaultLocationId is not null)
            {
                _ = await _db.Locations.FindAsync(item.DefaultLocationId) ??
                    throw new NotFoundException($"Locatie met ID: {item.DefaultLocationId} bestaat niet");
            }
           
            _db.Parts.Add(item);

            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Part? existing = await _db.Parts.FindAsync(id);

            if (existing is null) return false;

            List<string> relatedEntities = new();

            bool hasStockQuantity = await _db.StockItems.AnyAsync(s => s.PartId == id && s.Quantity > 0);
            if (hasStockQuantity) relatedEntities.Add("Onderdeel heeft nog voorraad");

            bool hasSupplier = await _db.PartSuppliers.AnyAsync(p => p.PartId == id);
            if (hasSupplier) relatedEntities.Add("Onderdeel heeft nog leverancier");

            if (hasStockQuantity || hasSupplier) throw new CannotDeleteException(existing.Name, relatedEntities);


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

            _ = await _db.Categories.FindAsync(item.CategoryId) ?? throw new NotFoundException($"Categorie met ID: {item.CategoryId} bestaat niet");

            if (item.DefaultLocationId is not null)
            {
                _ = await _db.Locations.FindAsync(item.DefaultLocationId) ??
                    throw new NotFoundException($"Locatie met ID: {item.DefaultLocationId} bestaat niet");
            }

            item.Sku = toBeUptdate.Sku;

            _db.Entry(toBeUptdate).CurrentValues.SetValues(item);

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
