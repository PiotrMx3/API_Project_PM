using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.CustomExceptions;
using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace API_Project_PM.Core.Services.Suppliers
{
    public class SupplierService : ISupplierRepository
    {
        private readonly AppDBContext _db;


        public SupplierService(AppDBContext db)
        {
            this._db = db;
        }

        public async Task<Supplier> CreateAsync(Supplier item)
        {
            Supplier? duplicateVatNumber = await _db.Suppliers.Where(s => s.VatNumber == item.VatNumber).FirstOrDefaultAsync();

            if (duplicateVatNumber is not null) throw new ConflictException("duplicaat van vat nummer");

            _db.Suppliers.Add(item);

            await _db.SaveChangesAsync();

            return item;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Supplier? result = await _db.Suppliers.FindAsync(id);
            if (result is null) return false;

            bool hasPartSupplier = await _db.PartSuppliers.AnyAsync(s => s.SupplierId == result.Id);

            if (hasPartSupplier) throw new CannotDeleteException("Supplier","parts");

            _db.Remove(result);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _db.Suppliers.ToListAsync();
        }


        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _db.Suppliers
                .Include(s => s.PartSuppliers)
                .ThenInclude(ps => ps.Part)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> UpdateAsync(Supplier item)
        {
            var toUpdate = await _db.Suppliers.FindAsync(item.Id);
            if (toUpdate is null) return false;

            if (await _db.Suppliers.AnyAsync(s => s.VatNumber == item.VatNumber && s.Id != item.Id)) throw new ConflictException($"Leverancier met: {item.VatNumber} bestaat al");

            _db.Entry(toUpdate).CurrentValues.SetValues(item);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
