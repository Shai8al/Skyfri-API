using Skyfri.Models;

namespace Skyfri.BL.IServices
{
    public interface IPortfolioService
    {
        Task<IEnumerable<Portfolio>> GetPortfolioByTenantIdAsync(Guid tenantId);
        Task<Portfolio> GetByTenantAndPortfolioIdAsync(Guid tenantId, Guid portfolioId);
        Task<Portfolio> GetPortfolioByIdAsync(Guid portfolioId);
        Task<Portfolio> AddPortfolioAsync(Portfolio portfolioEntity);
        Task DeletePortfolioAsync(Guid portfolioId);
    }
}

