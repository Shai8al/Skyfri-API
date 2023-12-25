namespace Skyfri.Models
{
    public class Plant
    {
        public Guid PlantId { get; set; }
        public Guid PortfolioId { get; set; }
        public string PlantName { get; set; }

        // Navigation property for the related Portfolio
        public virtual Portfolio Portfolio { get; set; }
    }
}
