using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.Suppliers
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliers();
        Task<Supplier?> GetSupplierById(int id);
        Task CreateSupplier(Supplier item);
        Task<bool> UpdateSupplier(int id, Supplier item);
        Task<bool> DeleteSupplier(int id);

    }
}
