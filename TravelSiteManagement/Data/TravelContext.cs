using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Models;

namespace TravelSiteWeb.Data
{
    public class TravelContext : IdentityDbContext
    {
        public TravelContext()
        {
        }

        public TravelContext(DbContextOptions<TravelContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<TravelDestination> TravelDestinations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<TravelDestination>().ToTable("TravelDestination");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
        }
    }
}
