using API_Project_PM.Models;

namespace API_Project_PM.Services.Locations
{
    public class InMemoryLocationsRepository : ILocationsRepository
    {
        private static readonly List<Location> _locations = new()
        {
            new Location { Id = 1, Code = "S01-R01-SG01-P01-B01-T00-V01" },
            new Location { Id = 2, Code = "S01-R01-SG01-P01-B02-T00-V01" },
            new Location { Id = 3, Code = "S01-R02-SG01-P02-B03-T00-V01" },

            // Overflow zone
            new Location { Id = 4, Code = "S02-R01-SG01-P01-B01-T00-V00" },
            new Location { Id = 5, Code = "S02-R01-SG02-P02-B04-T00-V00" }
        };
        public Task<IEnumerable<Location>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Location?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
