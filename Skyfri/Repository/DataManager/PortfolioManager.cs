using Microsoft.EntityFrameworkCore;
using Skyfri.data_access;
using Skyfri.Models;
using Skyfri.Repository.IDataManager;

namespace Skyfri.Repository.DataManager
{
    /// <summary>
    /// Provides data access operations for Portfolio.
    /// </summary>
    public class PortfolioManager : IPortfolioRepository
    {
        readonly SkyfriDbContext _skyfriDbContext;

        /// <summary>
        /// Constructor to initialize PortfolioManager.
        /// </summary>
        /// <param name="skyfriDbContext">Instance for skyfriDbContext.</param>
        public PortfolioManager(SkyfriDbContext skyfriDbContext)
        {
            _skyfriDbContext = skyfriDbContext;
        }

        /// <summary>
        /// Get list of Portfolios.
        /// </summary>
        /// <param name="tenantId">UID of the tenant.</param>
        /// <returns>List of all the portfolios associated with the tenant.</returns>
        public async Task<IEnumerable<Portfolio>> GetPortfolioByTenantIdAsync(Guid tenantId)
        {
            return await _skyfriDbContext.Portfolios.Where(e => e.TenantId == tenantId).ToListAsync();
        }

        /// <summary>
        /// Get portfolio by portfolio id.
        /// </summary>
        /// <param name="portfolioId">UID of the portfolio.</param>
        /// <returns>Portfolio data associated with the portfolio id.</returns>
        public async Task<Portfolio> GetPortfolioByPortfolioIdAsync(Guid portfolioId)
        {
            return await _skyfriDbContext.Portfolios
                  .FirstOrDefaultAsync(e => e.PortfolioId == portfolioId);
        }

        /// <summary>
        /// Add a portfolio.
        /// </summary>
        /// <param name="portfolioEntity">Details of the portfolio.</param>
        /// <returns>Added portfolio data.</returns>
        public async Task<Portfolio> AddPortfolioAsync(Portfolio portfolioEntity)
        {
            await _skyfriDbContext.Portfolios.AddAsync(portfolioEntity);
            await _skyfriDbContext.SaveChangesAsync();
            return portfolioEntity;
        }

        /// <summary>
        /// Delete aportfolio.
        /// </summary>
        /// <param name="portfolioEntity">Details of the portfolio</param>
        public async Task DeletePortfolioAsync(Portfolio portfolioEntity)
        {
            _skyfriDbContext.Portfolios.Remove(portfolioEntity);
            await _skyfriDbContext.SaveChangesAsync();
        }
    }
}
