using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Skyfri.ViewModels
{
    /// <summary>
    /// View model for Plant entity
    /// </summary>
    public class PlantViewModel
    {
        /// <summary>
        /// Id of the Plant
        /// </summary>
        public virtual Guid PlantId { get; set; }
        /// <summary>
        /// UID for the portfolio in which the plant is registered
        /// </summary>
        public virtual Guid PortfolioId { get; set; }
        /// <summary>
        /// Name of the plant
        /// </summary>
        [Required]
        public string PlantName { get; set; }
    }
    /// <summary>
    /// Update model for Plant entity
    /// </summary>
    public class PlantUpdateModel : PlantViewModel
    {
        /// <summary>
        /// Id of the Plant
        /// </summary>
        [JsonIgnore]
        public override Guid PlantId { get; set; }
        /// <summary>
        /// UID for the portfolio in which the plant is registered
        /// </summary>
        [JsonIgnore]
        public override Guid PortfolioId { get; set; }
    }
}
