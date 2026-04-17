using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.Suppliers
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(int id);
        Task<Supplier> CreateAsync(Supplier item);
        Task<bool> UpdateAsync(Supplier item);
        Task<bool> DeleteAsync(int id);

    }
}
