using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Project_PM.Core.Services.PartsSuppliers
{
    public class PartSupplierService : IPartSupplierRepository
    {

        public readonly AppDBContext _db;

        public PartSupplierService(AppDBContext db)
        {
            this._db = db;
        }



        public async Task<PartSupplier> CreateAsync(PartSupplier item)
        {
            await _db.PartSuppliers.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int partId, int supplierId)
        {
            PartSupplier? result = await _db.PartSuppliers.FirstOrDefaultAsync(ps => ps.PartId == partId && ps.SupplierId == supplierId);
            if (result is null) return false;

            _db.Remove(result);

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<PartSupplier?> GetById(int partId, int supplierId)
        {
            PartSupplier? result = await _db.PartSuppliers
                .Include(ps => ps.Part)
                .Include(ps => ps.Supplier)
                .FirstOrDefaultAsync(ps => ps.PartId == partId && ps.SupplierId == supplierId);

            return result;
        }

        public async Task<bool> UpdetAsync(PartSupplier item)
        {
            var toBeUptdate = await _db.PartSuppliers
                .FirstOrDefaultAsync(ps => ps.PartId ==item.PartId && ps.SupplierId == item.SupplierId);

            if (toBeUptdate is null) return false;

            _db.Entry(toBeUptdate).CurrentValues.SetValues(item);

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
