using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.Locations
{
    public interface ILocationsRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location?> GetByIdAsync(int id);
        Task<Location> CreateAsync(Location item);
        Task<bool> UpdateAsync(Location item);
        Task<bool> DeleteAsync(int id);

    }
}
