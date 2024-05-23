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

            //Client/Reservation
            //modelBuilder.Entity<ClientReservation>()
            //    .HasKey(cr => new { cr.ClientID, cr.ReservationID });
            //
            //modelBuilder.Entity<Client>()
            //    .HasMany(c => c.Reservations)
            //    .WithMany(r => r.Clients)
            //    .UsingEntity<ClientReservation>(
            //    rtg => rtg
            //        .HasOne(cr => cr.Reservations)
            //        .WithMany()
            //        .HasForeignKey(cr => cr.ReservationID),
            //    rtg => rtg
            //        .HasOne(cr => cr.Clients)
            //        .WithMany()
            //        .HasForeignKey(cr => cr.ClientID));
            //
            //modelBuilder.Entity<ReservationDestination>()
            //    .HasKey(rd => new { rd.ReservationID, rd.TravelDestinationID });
            ////Reservation/TravelDestination
            //
            ////TravelDestination/Hotel
            //modelBuilder.Entity<DestinationHotel>()
            //    .HasKey(dh => new { dh.TravelDestinationID, dh.HotelID });
            //
            //modelBuilder.Entity<TravelDestination>()
            //    .HasMany(td => td.Hotels)
            //    .WithMany(h => h.TravelDestinations)
            //    .UsingEntity<DestinationHotel>(
            //    rtg => rtg
            //        .HasOne(dh => dh.Hotels)
            //        .WithMany()
            //        .HasForeignKey(dh => dh.HotelID),
            //    rtg => rtg
            //        .HasOne(dh => dh.TravelDestinations)
            //        .WithMany()
            //        .HasForeignKey(dh => dh.TravelDestinationID));
            //
            ////TravelDestination/Flight
            //modelBuilder.Entity<TravelFlight>()
            //    .HasKey(tf => new { tf.FlightID, tf.TravelDestinationID });
            //modelBuilder.Entity<TravelDestination>()
            //    .HasMany(td => td.Flights)
            //    .WithMany(f => f.TravelDestinations)
            //    .UsingEntity<TravelFlight>(
            //    rtg => rtg
            //        .HasOne(tf => tf.Flights)
            //        .WithMany()
            //        .HasForeignKey(tf => tf.FlightID),
            //    rtg => rtg
            //        .HasOne(tf => tf.TravelDestinations)
            //        .WithMany()
            //        .HasForeignKey(tf => tf.TravelDestinationID));
            //
        }
    }
}
