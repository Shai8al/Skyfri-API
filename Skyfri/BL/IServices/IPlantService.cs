using Skyfri.Models;

namespace Skyfri.BL.IServices
{
    public interface IPlantService
    {
        Task<IEnumerable<Plant>> GetPlantsByPortfolioIdAsync(Guid portfolioId);
        Task<Plant> GetPlantsByPortfolioIdAndPlantAsync(Guid portfolioId, Guid plantId);
        Task<Plant> AddPlantAsync(Plant plantEntity);
        Task DeletePlantAsync(Guid plantId);
    }
}
