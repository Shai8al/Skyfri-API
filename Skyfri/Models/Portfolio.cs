namespace Skyfri.Models
{
    public class Portfolio
    {
        public Guid PortfolioId { get; set; }
        public Guid TenantId { get; set; }
        public string PortfolioName { get; set; }

        // Navigation property for the related Tenant
        public virtual Tenant Tenant { get; set; }

        public virtual List<Plant> plants { get; set; }
    }
}
