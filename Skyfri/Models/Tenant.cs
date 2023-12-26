﻿namespace Skyfri.Models
{
    public class Tenant
    {
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantCountry { get; set; }

        public virtual List<Portfolio> portfolios { get; set; }
    }
}
