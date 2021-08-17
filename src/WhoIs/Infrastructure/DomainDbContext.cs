using Microsoft.EntityFrameworkCore;
using WhoIs.Entities;

namespace WhoIs.Infrastructure
{
    public sealed class DomainDbContext : DbContext
    {
        public DbSet<Domain> Domains { get; set; }

        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.MapDomain();
        }
    }
}
