using API_Project_PM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Project_PM.Core.Services.PartsSuppliers
{
    public interface IPartSupplierRepository
    {
        Task<PartSupplier> CreateAsync(PartSupplier item);
        Task<bool> UpdetAsync(PartSupplier item);
        Task<PartSupplier?> GetById(int partId, int supplierId);
        Task<bool> DeleteAsync(int partId, int supplierId);
    }
}
