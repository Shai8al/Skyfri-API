using Microsoft.EntityFrameworkCore;
using Skyfri.Models;

namespace Skyfri.data_access
{
    public class SkyfriDbContext : DbContext
    {
        public SkyfriDbContext(DbContextOptions<SkyfriDbContext> options) : base(options) { }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Plant> Plants { get; set; }
    }
}
