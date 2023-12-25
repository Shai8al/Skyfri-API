using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Skyfri.ViewModels
{
    /// <summary>
    /// View model for tenant entity
    /// </summary>
    public class TenantViewModel
    {
        /// <summary>
        /// Id of the Tenant
        /// </summary>
        public virtual Guid TenantId { get; set; }
        /// <summary>
        /// Name of the tenant
        /// </summary>
        [Required]
        public string TenantName { get; set; }
        /// <summary>
        /// Country where the tenant is based at
        /// </summary>
        [Required]
        public string TenantCountry { get; set; }
    }
    /// <summary>
    /// Update model for tenant entity
    /// </summary>
    public class TenantUpdateModel : TenantViewModel
    {
        /// <summary>
        /// Id of the Tenant
        /// </summary>
        [JsonIgnore]
        public override Guid TenantId { get; set; }
    }
}
