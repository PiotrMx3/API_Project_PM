using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.Database;
using API_Project_PM.Core.Enums;
using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Core.Services.StockItems
{
    public class StockItemService : IStockItemRepository
    {
        private readonly AppDBContext _db;


        public StockItemService(AppDBContext db)
        {
            this._db = db;
        }

        public async Task<bool> DeleteAsync(int partId, int locationId)
        {
            StockItem? result = await _db.StockItems
                .Where(si => si.PartId == partId && si.LocationId == locationId)
                .FirstOrDefaultAsync();

            if (result is null) return false;
            if (result.Quantity > 0) throw new ConflictException("deze onderdeel heeft nog voorraad");
            _db.StockItems.Remove(result);
            await _db.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<StockItem>> GetAllAsync()
        {
            return await _db.StockItems
                .Include(si => si.Part)
                .Include(si => si.Location)
                .ToListAsync();

        }

        public async Task<IEnumerable<StockItem>> GetByLocationIdAsync(int locationId)
        {
            return await _db.StockItems
            .Include(si => si.Part)
            .Include(si => si.Location)
            .Where(si => si.LocationId == locationId)
            .ToListAsync();
        }

        public async Task<IEnumerable<StockItem>> GetByPartIdAsync(int partId)
        {
            return await _db.StockItems
               .Include(si => si.Part)
               .Include(si => si.Location)
               .Where(si => si.PartId == partId)
               .ToListAsync();
        }

        public async Task<bool> UpsertAsync(int partId, int locationId, int quantity, MovementType type)
        {

            Part? existingPart = await _db.Parts
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == partId)
                ?? throw new NotFoundException("Deze onderdeel bestaat niet");


            Location? existingLocation = await _db.Locations
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(l => l.Id == locationId)
                ?? throw new NotFoundException("Deze locatie bestaat niet");

            if (existingPart.IsDeleted) throw new ConflictException("Deze onderdeel is inactive ");
            if (existingLocation.IsDeleted) throw new ConflictException("Deze locatie is inactive ");

            StockItem? toBeUpdate = await _db.StockItems
                .Where(si => si.PartId == partId && si.LocationId == locationId)
                .FirstOrDefaultAsync();


            if (toBeUpdate is null && type == MovementType.Out) throw new NotFoundException("Er is geen voorrad op deze locatie");

            if (toBeUpdate is null)
            {
                _db.StockItems.Add(new StockItem 
                { 
                    LocationId = locationId,
                    PartId = partId,
                    Quantity = quantity
                });

                return true;
            }

            if (type == MovementType.In)
            {
                toBeUpdate.Quantity += quantity;
            }
            else
            {
                toBeUpdate.Quantity -= quantity;
                if (toBeUpdate.Quantity < 0) throw new ConflictException("Onvoldoende voorrad");
            }

            return true;
        }
    }
}
