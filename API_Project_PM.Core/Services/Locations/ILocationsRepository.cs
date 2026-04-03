using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.Locations
{
    public interface ILocationsRepository
    {
        Task<IEnumerable<Location>> GetAllLocations();
        Task<Location?> GetLocationById(int id);
        Task CreateLocation(Location item);
        Task<bool> UpdateLocation(int id, Location item);
        Task<bool> DeleteLocation(int id);

    }
}
