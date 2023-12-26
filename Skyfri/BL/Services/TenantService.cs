using Skyfri.BL.IServices;
using Skyfri.Models;
using Skyfri.Repository.IDataManager;

namespace Skyfri.BL.Services
{
    /// <summary>
    /// Service class handling tenant-related operations.
    /// </summary>
    public class TenantService : ITenantService
    {
        readonly ITenantRepository _tenantRepository;
        /// <summary>
        /// Constructor to initialize TenantService.
        /// </summary>
        /// <param name="tenantRepository">Instance for the tenant repository.</param>
        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        /// <summary>
        /// List of all tenants.
        /// </summary>
        /// <returns>list of all the registered tenants.</returns>
        public async Task<IEnumerable<Tenant>> GetAllTenantsAsync()
        {
            return await _tenantRepository.GetAllTenantsAsync();
        }

        /// <summary>
        /// Get the tenant by tenantID
        /// </summary>
        /// <param name="tenantId">UID of the tenant</param>
        /// <returns>tenant associated with the UID</returns>
        public async Task<Tenant> GetTenantByIdAsync(Guid tenantId)
        {
            return await _tenantRepository.GetTenantByIdAsync(tenantId);
        }

        /// <summary>
        /// Add a tenant
        /// </summary>
        /// <param name="tenantEntity">details of the tenant</param>
        /// <returns>the created tenant</returns>
        public async Task<Tenant> AddTenantAsync(Tenant tenantEntity)
        {
            return await _tenantRepository.AddTenantAsync(tenantEntity);
        }

        /// <summary>
        /// Update a tenant
        /// </summary>
        /// <param name="updatedTenant">updated data of the tenant</param>
        /// <param name="tenantEntity">exisiting data of the tenant</param>
        /// <returns>the updated tenant</returns>
        public async Task<Tenant> UpdateTenantAsync(Tenant updatedTenant, Tenant tenantEntity)
        {
            tenantEntity.TenantName = updatedTenant.TenantName;
            tenantEntity.TenantCountry = updatedTenant.TenantCountry;
            return await _tenantRepository.UpdateTenantAsync(tenantEntity);
        }

        /// <summary>
        /// Delete a tenant
        /// </summary>
        /// <param name="tenantId">UID associated with the tenant</param>
        public async Task DeleteTenantAsync(Guid tenantId)
        {
            Tenant tenantEntity = await _tenantRepository.GetTenantByIdAsync(tenantId);
            await _tenantRepository.DeleteTenantAsync(tenantEntity);
        }
    }
}
