using API_Minimal_Project_PM.Models;

namespace API_Minimal_Project_PM.Services.Suppliers
{
    public interface ISuppliersRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliers();
        Task<Supplier?> GetSupplierById(int id);
        Task CreateSupplier(Supplier item);
        Task<bool> UpdateSupplier(int id, Supplier item);
        Task<bool> DeleteSupplier(int id);

    }
}
