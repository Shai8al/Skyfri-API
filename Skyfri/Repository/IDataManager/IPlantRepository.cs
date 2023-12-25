using Skyfri.Models;

namespace Skyfri.Repository.IDataManager
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetPlantsByPortfolioIdAsync(Guid portfolioId);
        Task<Plant> GetPlantByPlantIdAsync(Guid plantId);
        Task<Plant> AddPlantAsync(Plant plantEntity);
        Task DeletePlantAsync(Plant plantEntity);
    }
}
