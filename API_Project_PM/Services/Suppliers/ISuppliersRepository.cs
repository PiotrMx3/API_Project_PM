using API_Project_PM.Models;

namespace API_Project_PM.Services.Suppliers
{
    public interface ISuppliersRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliers();
        Task<Supplier?> GetSupplierById(int id);
        Task CreateSupplier(Supplier item);

    }
}
