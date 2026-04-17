using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.Parts
{
    public interface IPartRepository
    {
        Task<IEnumerable<Part>> GetAllAsync();
        Task<Part?> GetByIdAsync(int id);
        Task<Part> CreateAsync(Part item);
        Task<bool> UpdateAsync(Part item);
        Task<bool> DeleteAsync(int id);

    }
}
