using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.Locations
{
    public class InMemoryLocationsRepository : ILocationsRepository
    {
        private static readonly List<Location> _locations = new()
    {
        new Location { Id = 1, Zone = "A", Rack = "12", Shelf = "1", Box = "101"},
        new Location { Id = 2, Zone = "A", Rack = "12", Shelf = "2", Box = "102"},
        new Location { Id = 3, Zone = "B", Rack = "05", Shelf = "4", Box = "250"},
        new Location { Id = 4, Zone = "C", Rack = "22", Shelf = "1", Box = "005"},
        new Location { Id = 5, Zone = "D", Rack = "01", Shelf = "3", Box = "999"}
    };

        public Task CreateLocation(Location item)
        {
            var id = _locations.LastOrDefault()?.Id ?? 0;

            item.Id = id + 1;

            _locations.Add(item);

            return Task.CompletedTask;
        }

        public Task<bool> DeleteLocation(int id)
        {
            Location? existing = _locations.FirstOrDefault(i => i.Id == id);
            if (existing is null) return Task.FromResult(false);
            _locations.Remove(existing);

            return Task.FromResult(true);
        }
        public Task<bool> UpdateLocation(int id, Location item)
        {
            Location? existing = _locations.FirstOrDefault(i => i.Id == id);
            if (existing is null) return Task.FromResult(false);

            existing.Zone = item.Zone;
            existing.Rack = item.Rack;
            existing.Shelf = item.Shelf;
            existing.Box = item.Box;

            return Task.FromResult(true);
        }

        public Task<IEnumerable<Location>> GetAllLocations()
        {
            return Task.FromResult(_locations.AsEnumerable());
        }

        public Task<Location?> GetLocationById(int id)
        {
            Location? result = _locations.FirstOrDefault(e => e.Id == id);

            if (result is null) return Task.FromResult<Location?>(null);

            return Task.FromResult<Location?>(result);
        }

    }
}
