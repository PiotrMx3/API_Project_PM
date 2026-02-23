using API_Project_PM.Models;

namespace API_Project_PM.Services.Parts
{
    public class InMemoryPartsRepository : IPartsRepository
    {
        private static readonly List<Part> _parts = new()
        {
            new Part { Id = 1, Sku = "FUSE-10A", Name = "Fuse 10A" },
            new Part { Id = 2, Sku = "FUSE-16A", Name = "Fuse 16A" },
            new Part { Id = 3, Sku = "RELAY-230V", Name = "Relay 230V" },
            new Part { Id = 4, Sku = "CONTACTOR-24V", Name = "Contactor 24V" },
            new Part { Id = 5, Sku = "MCB-C16", Name = "MCB C16" }
        };
        public Task<IEnumerable<Part>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Part?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
