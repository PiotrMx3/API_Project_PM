using API_Project_PM.Models;

namespace API_Project_PM.Services.Locations
{
    public interface ILocationsRepository
    {
        Task<IEnumerable<Location>> GetAllLocations();
        Task<Location?> GetLocationById(int id);
        Task CreateLocation(Location item);

    }
}
