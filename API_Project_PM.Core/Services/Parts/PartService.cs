using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.Parts
{
    public class PartService : IPartRepository
    {
        private readonly AppDBContext _db;

        public PartService(AppDBContext db)
        {
            this._db = db;
        }
        public Task<Part?> CreateAsync(Part item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Part>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Part?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Part item)
        {
            throw new NotImplementedException();
        }
    }
}
