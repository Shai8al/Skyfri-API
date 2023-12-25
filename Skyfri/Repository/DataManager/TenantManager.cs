using Microsoft.EntityFrameworkCore;
using Skyfri.data_access;
using Skyfri.Models;
using Skyfri.Repository.IDataManager;

namespace Skyfri.Repository.DataManager
{
    /// <summary>
    /// Provides data access operations for Tenant.
    /// </summary>
    public class TenantManager : ITenantRepository
    {
        readonly SkyfriDbContext _skyfriDbContext;

        /// <summary>
        /// Constructor to initialize TenantManager.
        /// </summary>
        /// <param name="skyfriDbContext">Instance for skyfriDbContext.</param>
        public TenantManager(SkyfriDbContext skyfriDbContext) 
        {
            _skyfriDbContext = skyfriDbContext;
        }

        /// <summary>
        /// Get list of all Tenants.
        /// </summary>
        /// <returns>List of all the tenants</returns>
        public async Task<IEnumerable<Tenant>> GetAllTenantsAsync()
        {
            return await _skyfriDbContext.Tenants.ToListAsync();
        }

        /// <summary>
        /// Get tenant by tenant id.
        /// </summary>
        /// <param name="tenantId">UID of the tenant.</param>
        /// <returns>Tenant data associated with the tenantId.</returns>
        public async Task<Tenant> GetTenantByIdAsync(Guid tenantId)
        {
            return await _skyfriDbContext.Tenants
                  .FirstOrDefaultAsync(e => e.TenantId == tenantId);
        }

        /// <summary>
        /// Adding a tenant.
        /// </summary>
        /// <param name="tenantEntity">Details of the tenant.</param>
        /// <returns>Added tenant data.</returns>
        public async Task<Tenant> AddTenantAsync(Tenant tenantEntity)
        {
            await _skyfriDbContext.Tenants.AddAsync(tenantEntity);
            await _skyfriDbContext.SaveChangesAsync();
            return tenantEntity;
        }

        /// <summary>
        /// Updated tenant details
        /// </summary>
        /// <param name="newTenantEntity">New tenant details.</param>
        /// <returns>Updated tenant data.</returns>
        public async Task<Tenant> UpdateTenantAsync(Tenant newTenantEntity)
        {
            await _skyfriDbContext.SaveChangesAsync();
            return newTenantEntity;
        }

        /// <summary>
        /// Delete a tenant.
        /// </summary>
        /// <param name="tenantEntity">Details of the tenant.</param>
        public async Task DeleteTenantAsync(Tenant tenantEntity)
        {
            _skyfriDbContext.Tenants.Remove(tenantEntity);
            await _skyfriDbContext.SaveChangesAsync();
        }
    }
}
