using Skyfri.Models;

namespace Skyfri.Repository.IDataManager
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Portfolio>> GetPortfolioByTenantIdAsync(Guid tenantId);
        Task<Portfolio> GetPortfolioByPortfolioIdAsync(Guid portfolioId);
        Task<Portfolio> AddPortfolioAsync(Portfolio portfolioEntity);
        Task DeletePortfolioAsync(Portfolio portfolioEntity);
    }
}
