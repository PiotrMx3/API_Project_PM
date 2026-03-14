using API_Project_PM.Models;

namespace API_Project_PM.Services
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category?> GetById(int id);
    }
}

