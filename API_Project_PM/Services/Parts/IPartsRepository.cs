using API_Project_PM.Models;

namespace API_Project_PM.Services.Parts
{
    public interface IPartsRepository
    {
        Task<IEnumerable<Part>> GetAllParts();
        Task<Part?> GetPartById(int id);
        Task CreatePart(Part item);

    }
}
