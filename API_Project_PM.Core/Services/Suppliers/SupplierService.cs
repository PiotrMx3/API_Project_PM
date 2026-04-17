using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace API_Project_PM.Core.Services.Suppliers
{
    public class SupplierService : ISupplierRepository
    {
        public readonly AppDBContext _db;


        public SupplierService(AppDBContext db)
        {
            this._db = db;
        }

        public async Task<Supplier> CreateAsync(Supplier item)
        {
            _db.Suppliers.Add(item);

            await _db.SaveChangesAsync();

            return item;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Supplier? result = await _db.Suppliers.FindAsync(id);

            if (result is null) return false;


            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _db.Suppliers.ToListAsync();
        }


        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _db.Suppliers.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Supplier item)
        {
            var toUpdate = await _db.Suppliers.FindAsync(item.Id);

            if (toUpdate is null) return false;

            _db.Entry(toUpdate).CurrentValues.SetValues(item);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
