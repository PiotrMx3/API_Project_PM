using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.Categories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category item);
        Task<bool> UpdateAsync(Category item);
        Task<bool> DeleteAsync(int id);
    }
}

