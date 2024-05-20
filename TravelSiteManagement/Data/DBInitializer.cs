using System.Diagnostics;
using TravelSiteWeb.Models;
using System;
using System.Linq;

namespace TravelSiteWeb.Data
{
    public class DBInitializer
    {
        public static void Initialize(TravelContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }

            var clients = new Client[]
            {
            new Client{FirstName="Artur",LastName="Zelek",TravelDate=DateTime.Parse("2005-09-01"), Address="Gandora 41", PhoneNumber="792413164"},
            new Client{FirstName="Oskar",LastName="Walaszczyk",TravelDate=DateTime.Parse("2005-09-01"), Address = "Willowa 2",PhoneNumber="792413164"},
            new Client{FirstName="Dawid",LastName="Waluszek",TravelDate=DateTime.Parse("2005-09-01"), Address = "Willowa 2",PhoneNumber="792413164"},
            new Client{FirstName="Krzysztof",LastName="Krawczyk",TravelDate=DateTime.Parse("2005-09-01"), Address = "Willowa 2",PhoneNumber="792413164"},
            new Client{FirstName="Rudy",LastName="Plecowłochowski",TravelDate=DateTime.Parse("2005-09-01"), Address = "Willowa 2",PhoneNumber="792413164"}
            };
            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {
            new Reservation{ReservationID=1,Title="Italian Delights Express",Cost=800, Description="Italia"},
            new Reservation{ReservationID=2,Title="A Spanish Flamenco",Cost=1100,Description = "Spain"},
            new Reservation{ReservationID=3,Title="Turkish Delights",Cost=1200, Description = "Turkey"},
            new Reservation{ReservationID=4,Title="Mythical Greece Wonders",Cost=1300,Description = "Greece"},
            new Reservation{ReservationID=5,Title="Barcelona",Cost=1400,Description = "Spain"},
            new Reservation{ReservationID=6,Title="Istanbul",Cost=700,Description = "Turkey"},
            new Reservation{ReservationID=7,Title="Sparta",Cost=850, Description = "Greece"},
            new Reservation{ReservationID=8,Title="Mediolan",Cost=800, Description = "Italia"}
            
                /*new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F}, */
          
            };
            foreach (Reservation r in reservations)
            {
                context.Reservations.Add(r);
            }
            context.SaveChanges();

            var travels = new TravelDestination[]
            {
            new TravelDestination{TravelDestinationID = 1, Country = Country.Italy,Cost=1000,City="Barcelona"},
            new TravelDestination{TravelDestinationID = 2, Country = Country.Greece,Cost = 1200,City = "Barcelona"},
            new TravelDestination{TravelDestinationID = 3, Country = Country.Spain,Cost = 11000, City = "Barcelona"},
            new TravelDestination{TravelDestinationID = 4, Country = Country.Turkey,Cost = 1300,City = "Barcelona"},
            new TravelDestination{TravelDestinationID = 6, Country = Country.Italy,Cost = 1500, City = "Barcelona"},
            new TravelDestination{TravelDestinationID = 7, Country = Country.Italy,Cost = 600, City = "Barcelona"},
            new TravelDestination{TravelDestinationID = 8, Country = Country.Spain,Cost = 9500 , City = "Barcelona"}
            };
            foreach (TravelDestination td in travels)
            {
                context.TravelDestinations.Add(td);
            }
            context.SaveChanges();
        }
    }
}
