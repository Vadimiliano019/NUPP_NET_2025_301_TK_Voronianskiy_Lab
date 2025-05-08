using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Models;

namespace Transport.Infrastructure
{
    public class TransportContext : DbContext
    {
        public DbSet<BusModel> Buses => Set<BusModel>();
        public DbSet<DriverModel> Drivers => Set<DriverModel>();
        public DbSet<RouteModel> Routes => Set<RouteModel>();

        public TransportContext(DbContextOptions<TransportContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Один до одного між Bus і Driver
            modelBuilder.Entity<DriverModel>()
                .HasOne(d => d.Bus)
                .WithOne(b => b.Driver)
                .HasForeignKey<BusModel>(b => b.DriverId);

            // Один до багатьох між Bus і Route
            modelBuilder.Entity<BusModel>()
                .HasMany(b => b.Routes)
                .WithOne(r => r.Bus)
                .HasForeignKey(r => r.BusId);
        }
    }
}
