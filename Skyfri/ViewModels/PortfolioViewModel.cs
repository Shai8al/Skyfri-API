using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Skyfri.ViewModels
{
    /// <summary>
    /// View model for Entity
    /// </summary>
    public class PortfolioViewModel
    {
        /// <summary>
        /// Id of the Portfolio
        /// </summary>
        public virtual Guid PortfolioId { get; set; }
        /// <summary>
        /// tenant UID for the portfolio
        /// </summary>
        public virtual Guid TenantId { get; set; }
        /// <summary>
        /// Name of the portfolio
        /// </summary>
        [Required]
        public string PortfolioName { get; set; }
    }
    /// <summary>
    /// Update model for portfolio entity
    /// </summary>
    public class PortfolioUpdateModel : PortfolioViewModel
    {
        /// <summary>
        /// Id of the Tenant
        /// </summary>
        [JsonIgnore]
        public override Guid PortfolioId { get; set; }
        /// <summary>
        /// tenant UID for the portfolio
        /// </summary>
        [JsonIgnore]
        public override Guid TenantId { get; set; }
    }
}
