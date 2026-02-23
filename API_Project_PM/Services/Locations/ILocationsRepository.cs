using API_Project_PM.Models;

namespace API_Project_PM.Services.Locations
{
    public interface ILocationsRepository
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location?> GetById(int id);
    }
}
