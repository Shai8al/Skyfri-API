using Skyfri.BL.IServices;
using Skyfri.Models;
using Skyfri.Repository.IDataManager;

namespace Skyfri.BL.Services
{
    /// <summary>
    /// Service class handling plant-related operations.
    /// </summary>
    public class PlantService : IPlantService
    {
        readonly IPlantRepository _plantRepository;
        /// <summary>
        /// Constructor to initialize PlantService
        /// </summary>
        /// <param name="plantRepository">Instance of plantrepository</param>
        public PlantService(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        /// <summary>
        /// Get all plants by portfolio.
        /// </summary>
        /// <param name="portfolioId">UID of the portfolio.</param>
        /// <returns>list of all plants.</returns>
        public async Task<IEnumerable<Plant>> GetPlantsByPortfolioIdAsync(Guid portfolioId)
        {
            return await _plantRepository.GetPlantsByPortfolioIdAsync(portfolioId);
        }

        /// <summary>
        /// Get plant by portfolioId and plantId.
        /// </summary>
        /// <param name="portfolioId">UID of the portfolio.</param>
        /// <param name="plantId">UID of the plant.</param>
        /// <returns>Plant associated with the UIDs</returns>
        public async Task<Plant> GetPlantsByPortfolioIdAndPlantAsync(Guid portfolioId, Guid plantId)
        {
            var plants = await _plantRepository.GetPlantsByPortfolioIdAsync(portfolioId);
            var plant = (plants.Count() > 0 || plants != null) ? plants.FirstOrDefault(x => x.PlantId == plantId) : null;
            return plant;
        }

        /// <summary>
        /// Add a plant.
        /// </summary>
        /// <param name="plantEntity">Data for the plant</param>
        /// <returns>Created plant entity</returns>
        public async Task<Plant> AddPlantAsync(Plant plantEntity)
        {
            return await _plantRepository.AddPlantAsync(plantEntity);
        }

        /// <summary>
        /// Delete a plant
        /// </summary>
        /// <param name="plantId">UID of the plant</param>
        /// <returns>NA</returns>
        public async Task DeletePlantAsync(Guid plantId)
        {
            Plant plantEntity = await _plantRepository.GetPlantByPlantIdAsync(plantId);
            await _plantRepository.DeletePlantAsync(plantEntity);
        }
    }
}
