using API_Project_PM.Core.Services.Categories;
using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace API_Project_PM.Core.Services.Categories
{
    public class CategoryService : ICategoryRepository
    {

        private readonly AppDBContext _db;

        public CategoryService(AppDBContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<Category> CreateAsync(Category item)
        {
                _db.Categories.Add(item);
                await _db.SaveChangesAsync();
                return item;
        }
        public async Task<bool> UpdateAsync(Category item)
        {
            var result = await _db.Categories.FindAsync(item.Id);
            if (result is null) return false;


            _db.Entry(result).CurrentValues.SetValues(item);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _db.Categories.FindAsync(id);
            if (result is null) return false;

            _db.Categories.Remove(result);
            await _db.SaveChangesAsync();

            return true;


        }



    }
}
