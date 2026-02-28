using API_Project_PM.Models;

namespace API_Project_PM.Services.Locations
{
    public class InMemoryLocationsRepository : ILocationsRepository
    {
        private static List<Location> _locations = new()
    {
        new Location { Id = 1, Aisle = "A", Rack = "12", Shelf = "1", Box = "101"},
        new Location { Id = 2, Aisle = "A", Rack = "12", Shelf = "2", Box = "102"},
        new Location { Id = 3, Aisle = "B", Rack = "05", Shelf = "4", Box = "250"},
        new Location { Id = 4, Aisle = "C", Rack = "22", Shelf = "1", Box = "005"},
        new Location { Id = 5, Aisle = "D", Rack = "01", Shelf = "3", Box = "999"}
    };
        public Task<IEnumerable<Location>> GetAll()
        {
            return Task.FromResult(_locations.AsEnumerable());
        }

        public Task<Location?> GetById(int id)
        {
            Location? result = _locations.FirstOrDefault(e => e.Id == id);

            return Task.FromResult(result);
        }
    }
}
