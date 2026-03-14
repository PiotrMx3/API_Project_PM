using API_Project_PM.Models;

namespace API_Project_PM.Services
{
    public class InMemeoryCategoryRepository : ICategoryRepository
    {
        private static readonly List<Category> Category = new()
        {
            new Category {Id = 1, Name = "Elektriciteit" },
            new Category {Id = 2, Name = "IJzerwaren" },
            new Category {Id = 3, Name = "Sanitair" },
            new Category {Id = 4, Name = "Pneumatiek" },

        };

        public Task<IEnumerable<Category>> GetAll()
        {
            return Task.FromResult(Category.AsEnumerable());
        }

        public Task<Category?> GetById(int id)
        {
            Category? result = Category.FirstOrDefault(c => c.Id == id);

            if (result is null) return Task.FromResult<Category?>(null);

            return Task.FromResult<Category?>(result);
        }
    }
}
