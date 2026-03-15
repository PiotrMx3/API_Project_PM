using API_Project_PM.Models;

namespace API_Project_PM.Services
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task CreateCategory(Category item);
        Task<bool> UpdateCategory(int id, Category item);
        Task<bool> DeleteCategory(int id);
    }
}

