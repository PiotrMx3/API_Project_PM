using API_Project_PM.Models;

namespace API_Project_PM.Services.Parts
{
    public interface IPartsRepository
    {
        Task<IEnumerable<Part>> GetAll();
        Task<Part?> GetById(int id);
    }
}
