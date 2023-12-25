using Skyfri.BL.IServices;
using Skyfri.Models;
using Skyfri.Repository.IDataManager;

namespace Skyfri.BL.Services
{
    /// <summary>
    /// Service class handling portfolio-related operations.
    /// </summary>
    public class PortfolioService : IPortfolioService
    {
        readonly IPortfolioRepository _portfolioRepository;

        /// <summary>
        /// Constructor to initialize PortfolioService.
        /// </summary>
        /// <param name="portfolioRepository">The repository for portfolio operations.</param>
        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        /// <summary>
        /// Get all portfolios for a tenant.
        /// </summary>
        /// <param name="tenantId">UID of the tenant.</param>
        /// <returns>collection of portfolios.</returns>
        public async Task<IEnumerable<Portfolio>> GetPortfolioByTenantIdAsync(Guid tenantId)
        {
            return await _portfolioRepository.GetPortfolioByTenantIdAsync(tenantId);
        }

        /// <summary>
        /// Get portfolio by tenant and portfolio ID.
        /// </summary>
        /// <param name="tenantId">UID of the tenant.</param>
        /// <param name="portfolioId">UID of the portfolio.</param>
        /// <returns>Portfolio associated with the given UIDs.</returns>
        public async Task<Portfolio> GetByTenantAndPortfolioIdAsync(Guid tenantId, Guid portfolioId)
        {
            var portfolios = await _portfolioRepository.GetPortfolioByTenantIdAsync(tenantId);
            var portfolio = (portfolios != null && portfolios.Any()) ? portfolios.FirstOrDefault(x => x.PortfolioId == portfolioId) : null;
            return portfolio;
        }

        /// <summary>
        /// Get portfolio by its ID.
        /// </summary>
        /// <param name="portfolioId">UID of the portfolio.</param>
        /// <returns>portfolio associated with the given UID.</returns>
        public async Task<Portfolio> GetPortfolioByIdAsync(Guid portfolioId)
        {
            return await _portfolioRepository.GetPortfolioByPortfolioIdAsync(portfolioId);
        }

        /// <summary>
        /// Add a new portfolio.
        /// </summary>
        /// <param name="portfolioEntity">The portfolio entity to be added.</param>
        /// <returns>The added portfolio.</returns>
        public async Task<Portfolio> AddPortfolioAsync(Portfolio portfolioEntity)
        {
            return await _portfolioRepository.AddPortfolioAsync(portfolioEntity);
        }

        /// <summary>
        /// Delete a portfolio.
        /// </summary>
        /// <param name="portfolioId">UID of the portfolio to be deleted.</param>
        public async Task DeletePortfolioAsync(Guid portfolioId)
        {
            Portfolio portfolioEntity = await _portfolioRepository.GetPortfolioByPortfolioIdAsync(portfolioId);
            if (portfolioEntity != null)
            {
                await _portfolioRepository.DeletePortfolioAsync(portfolioEntity);
            }
        }
    }
}
