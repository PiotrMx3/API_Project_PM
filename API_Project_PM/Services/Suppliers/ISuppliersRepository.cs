using API_Project_PM.Models;

namespace API_Project_PM.Services.Suppliers
{
    public interface ISuppliersRepository
    {
        Task<IEnumerable<Supplier>> GetAll();
        Task<Supplier?> GetById(int id);
    }
}
