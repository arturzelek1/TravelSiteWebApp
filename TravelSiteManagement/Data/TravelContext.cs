using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelSiteWeb.Models;
using System.Linq;

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
        //public DbSet<ClientReservation> ClientReservations { get; set; } //Join table Client/Reservation
        public DbSet<Reservation> Reservations { get; set; }
        //public DbSet<ReservationDestination> ReservationDestinations { get; set; } //Unecessary cause one to many
        public DbSet<TravelDestination> TravelDestinations { get; set; }
        //public DbSet<DestinationHotel> DestinationHotels { get; set; } //Join table TravelDestination/Hotel
        public DbSet<Hotel> Hotels { get; set; }
        //public DbSet<TravelFlight> TravelFlights { get; set; } //Join table TravelDestination/Flight
        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<TravelDestination>().ToTable("TravelDestination");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Flight>().ToTable("Flight");
            modelBuilder.Entity<Hotel>().ToTable("Hotel");

            modelBuilder.Entity<Flight>()
                .Property(f => f.FlightCost)
                .HasColumnType("decimal(18, 2)"); 

            modelBuilder.Entity<Hotel>()
                .Property(h => h.CostPerNight)
                .HasColumnType("decimal(18, 2)"); 
            modelBuilder.Entity<Reservation>()
                .Property(r => r.Cost)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
