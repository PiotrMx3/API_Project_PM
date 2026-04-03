using API_Project_PM.Core.Models;

namespace API_Project_PM.Core.Services.Parts
{
    public interface IPartsRepository
    {
        Task<IEnumerable<Part>> GetAllParts();
        Task<Part?> GetPartById(int id);
        Task CreatePart(Part item);
        Task<bool> UpdatePart(int id, Part item);
        Task<bool> DeletePart(int id);

    }
}
