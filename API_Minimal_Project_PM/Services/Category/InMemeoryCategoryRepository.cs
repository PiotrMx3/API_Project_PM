using API_Minimal_Project_PM.Models;

namespace API_Minimal_Project_PM.Services.Categories
{
    public class InMemeoryCategoryRepository : ICategoryRepository
    {
        private static readonly List<Category> _categories = new()
        {
            new Category {Id = 1, Name = "Elektriciteit" },
            new Category {Id = 2, Name = "IJzerwaren" },
            new Category {Id = 3, Name = "Sanitair" },
            new Category {Id = 4, Name = "Pneumatiek" },

        };

        public Task CreateCategory(Category item)
        {
            var id = _categories.LastOrDefault()?.Id ?? 0;

            item.Id = id + 1;

            _categories.Add(item);

            return Task.CompletedTask;
        }

        public Task<bool> DeleteCategory(int id)
        {
            Category? existing = _categories.FirstOrDefault(i => i.Id == id);
            if (existing is null) return Task.FromResult(false);

            _categories.Remove(existing);

            return Task.FromResult(true);
        }
        public Task<bool> UpdateCategory(int id, Category item)
        {
            Category? existing = _categories.FirstOrDefault(i => i.Id == id);
            if (existing is null) return Task.FromResult(false);

            existing.Name = item.Name;
            return Task.FromResult(true);

        }

        public Task<IEnumerable<Category>> GetAllCategories()
        {
            return Task.FromResult(_categories.AsEnumerable());
        }

        public Task<Category?> GetCategoryById(int id)
        {
            Category? result = _categories.FirstOrDefault(c => c.Id == id);

            if (result is null) return Task.FromResult<Category?>(null);

            return Task.FromResult<Category?>(result);
        }

    }
}
