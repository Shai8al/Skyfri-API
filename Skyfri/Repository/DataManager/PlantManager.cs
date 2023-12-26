using Microsoft.EntityFrameworkCore;
using Skyfri.data_access;
using Skyfri.Models;
using Skyfri.Repository.IDataManager;

namespace Skyfri.Repository.DataManager
{
    /// <summary>
    /// Provides data access operations for Plants.
    /// </summary>
    public class PlantManager : IPlantRepository
    {
        readonly SkyfriDbContext _skyfriDbContext;

        /// <summary>
        /// Constructor to initialize PlantManager.
        /// </summary>
        /// <param name="skyfriDbContext">Instance for skyfriDbContext.</param>
        public PlantManager(SkyfriDbContext skyfriDbContext)
        {
            _skyfriDbContext = skyfriDbContext;
        }

        /// <summary>
        /// List of Plants.
        /// </summary>
        /// <param name="portfolioId">UID of the portfolio.</param>
        /// <returns>List of plants associated with the portfolio.</returns>
        public async Task<IEnumerable<Plant>> GetPlantsByPortfolioIdAsync(Guid portfolioId)
        {
            return await _skyfriDbContext.Plants.Where(e => e.PortfolioId == portfolioId).ToListAsync();
        }

        /// <summary>
        /// Get plant by plant id.
        /// </summary>
        /// <param name="plantId">UID of the plant.</param>
        /// <returns>Plant data associated with the plantId.</returns>
        public async Task<Plant> GetPlantByPlantIdAsync(Guid plantId)
        {
            return await _skyfriDbContext.Plants
                  .FirstOrDefaultAsync(e => e.PlantId == plantId);
        }

        /// <summary>
        /// Add a plant.
        /// </summary>
        /// <param name="plantEntity">Details of the plant.</param>
        /// <returns>Created plant data.</returns>
        public async Task<Plant> AddPlantAsync(Plant plantEntity)
        {
            await _skyfriDbContext.Plants.AddAsync(plantEntity);
            await _skyfriDbContext.SaveChangesAsync();
            return plantEntity;
        }

        /// <summary>
        /// Delete a plant.
        /// </summary>
        /// <param name="plantEntity">Details of the plant.</param>
        public async Task DeletePlantAsync(Plant plantEntity)
        {
            _skyfriDbContext.Plants.Remove(plantEntity);
            await _skyfriDbContext.SaveChangesAsync();
        }
    }
}
