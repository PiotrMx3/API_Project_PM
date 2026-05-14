using API_Project_PM.Core.Models;


namespace API_Project_PM.Core.Services.PartsSuppliers
{
    public interface IPartSupplierRepository
    {
        Task<IEnumerable<PartSupplier>> GetAllAsync();
        Task<PartSupplier> CreateAsync(PartSupplier item);
        Task<bool> UpdetAsync(PartSupplier item);
        Task<PartSupplier?> GetById(int partId, int supplierId);
        Task<bool> DeleteAsync(int partId, int supplierId);
    }
}
