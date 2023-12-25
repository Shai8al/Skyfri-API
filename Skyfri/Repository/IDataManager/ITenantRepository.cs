using Skyfri.Models;

namespace Skyfri.Repository.IDataManager
{
    public interface ITenantRepository
    {
        Task<IEnumerable<Tenant>> GetAllTenantsAsync();
        Task<Tenant> GetTenantByIdAsync(Guid tenantId);
        Task<Tenant> AddTenantAsync(Tenant tenantEntity);
        Task<Tenant> UpdateTenantAsync(Tenant newTenantEntity);
        Task DeleteTenantAsync(Tenant tenantEntity);
    }
}
