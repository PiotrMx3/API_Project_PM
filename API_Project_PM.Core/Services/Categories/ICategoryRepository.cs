using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Categories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category item);
        Task<bool> UpdateAsync(int id, Category item);
        Task<bool> DeleteAsync(int id);
    }
}

