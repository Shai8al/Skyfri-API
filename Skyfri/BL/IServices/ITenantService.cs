using Skyfri.Models;
using Skyfri.ViewModels;

namespace Skyfri.BL.IServices
{
    public interface ITenantService
    {
        Task<IEnumerable<Tenant>> GetAllTenantsAsync();
        Task<Tenant> GetTenantByIdAsync(Guid tenantId);
        Task<Tenant> AddTenantAsync(Tenant tenantEntity);
        Task<Tenant> UpdateTenantAsync(Tenant updatedTenant, Tenant tenantEntity);
        Task DeleteTenantAsync(Guid tenantId);
    }
}
