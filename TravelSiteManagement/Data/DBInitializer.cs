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
            new Client{FirstName="Artur",LastName="Zelek", Address="Gandora 41", PhoneNumber="792413164"},
            new Client{FirstName="Oskar",LastName="Walaszczyk",Address = "Willowa 2",PhoneNumber="792413164"},
            new Client{FirstName="Dawid",LastName="Waluszek",Address = "Willowa 2",PhoneNumber="792413164"},
            new Client{FirstName="Krzysztof",LastName="Krawczyk", Address = "Willowa 2",PhoneNumber="792413164"},
            new Client{FirstName="Rudy",LastName="Plecowłochowski", Address = "Willowa 2",PhoneNumber="792413164"}
            };
            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            if (context.Reservations.Any())
            {
                return;   // DB has been seeded
            }
            var reservations = new Reservation[]
            {
            new Reservation{Title="Italian Delights Express",Cost=800, Description="Italia"},
            new Reservation{Title="A Spanish Flamenco",Cost=1100,Description = "Spain"},
            new Reservation{Title="Turkish Delights",Cost=1200, Description = "Turkey"},
            new Reservation{Title="Mythical Greece Wonders",Cost=1300,Description = "Greece"},
            new Reservation{Title="Barcelona",Cost=1400,Description = "Spain"},
            new Reservation{Title="Istanbul",Cost=700,Description = "Turkey"},
            new Reservation{Title="Sparta",Cost=850, Description = "Greece"},
            new Reservation{Title="Mediolan",Cost=800, Description = "Italia"}
          
            };
            foreach (Reservation r in reservations)
            {
                context.Reservations.Add(r);
            }
            context.SaveChanges();
            /*
            if (context.TravelDestinations.Any())
            {
                return;   // DB has been seeded
            }
            var travels = new TravelDestination[]
            {
            new TravelDestination{DateStart=DateTime.Parse("2005-09-01"),DateEnd=DateTime.Parse("2005-09-10"),City="Barcelona"},
            new TravelDestination{DateStart=DateTime.Parse("2005-09-01"),DateEnd=DateTime.Parse("2005-09-10"),City = "Barcelona"},
            new TravelDestination{DateStart = DateTime.Parse("2005-09-01"), DateEnd = DateTime.Parse("2005-09-10"), City = "Barcelona"},
            new TravelDestination{DateStart = DateTime.Parse("2005-09-01"), DateEnd = DateTime.Parse("2005-09-10"),City = "Barcelona"},
            new TravelDestination{DateStart = DateTime.Parse("2005-09-01"), DateEnd = DateTime.Parse("2005-09-10"), City = "Barcelona"},
            new TravelDestination{DateStart = DateTime.Parse("2005-09-01"), DateEnd = DateTime.Parse("2005-09-10"), City = "Barcelona"},
            new TravelDestination{DateStart = DateTime.Parse("2005-09-01"), DateEnd = DateTime.Parse("2005-09-10") , City = "Barcelona"}
            };
            foreach (TravelDestination td in travels)
            {
                context.TravelDestinations.Add(td);
            }
            context.SaveChanges();
            */
            if (context.Flights.Any())
            {
                return; // DB has been seeded
            }

            var flights = new Flight[]
            {
            new Flight { FlightNumber = "LO123", DepartureDate = DateTime.Parse("2022-01-01"), ArrivalDate = DateTime.Parse("2022-01-02"), FromLocation = "Warszawa", ToLocation = "Kraków", FlightCost = 200.00m, City = "Warszawa" },
            new Flight { FlightNumber = "LO456", DepartureDate = DateTime.Parse("2022-01-05"), ArrivalDate = DateTime.Parse("2022-01-06"), FromLocation = "Gdańsk", ToLocation = "Wrocław", FlightCost = 250.00m, City = "Gdańsk" },
            new Flight { FlightNumber = "LO789", DepartureDate = DateTime.Parse("2022-01-10"), ArrivalDate = DateTime.Parse("2022-01-11"), FromLocation = "Katowice", ToLocation = "Poznań", FlightCost = 180.00m, City = "Katowice" },
            new Flight { FlightNumber = "LO901", DepartureDate = DateTime.Parse("2022-01-15"), ArrivalDate = DateTime.Parse("2022-01-16"), FromLocation = "Łódź", ToLocation = "Szczecin", FlightCost = 220.00m, City = "Łódź" },
            new Flight { FlightNumber = "LO234", DepartureDate = DateTime.Parse("2022-01-20"), ArrivalDate = DateTime.Parse("2022-01-21"), FromLocation = "Bydgoszcz", ToLocation = "Białystok", FlightCost = 200.00m, City = "Bydgoszcz" }
            };

            foreach (Flight f in flights)
            {
                context.Flights.Add(f);
            }
            context.SaveChanges();

            if (context.Hotels.Any())
            {
                return; // DB has been seeded
            }

            var hotels = new Hotel[]
            {
            new Hotel { HotelName = "Hotel New York", Address = "700 5th Ave, New York, NY 10019", PostalCode = "10019", City = "Nowy Jork", Country = "Stany Zjednoczone", PhoneNumber = "123456789", Email = "hotelnewyork@example.com", CostPerNight = 300.00m, Website = "https://hotelnewyork.com", Description = "Pięciogwiazdkowy hotel w centrum Nowego Jorku", Image = "hotelnewyork.jpg" },
            new Hotel { HotelName = "Hotel Paryż", Address = "21 Rue de la Paix, 75002 Paris, Francja", PostalCode = "75002", City = "Paryż", Country = "Francja", PhoneNumber = "987654321", Email = "hotelparis@example.com", CostPerNight = 400.00m, Website = "https://hotelparis.com", Description = "Luksusowy hotel w centrum Paryża", Image = "hotelparis.jpg" },
            new Hotel { HotelName = "Hotel Londyn", Address = "10 Downing St, London, W1A 2AD, Wielka Brytania", PostalCode = "W1A 2AD", City = "Londyn", Country = "Wielka Brytania", PhoneNumber = "456789123", Email = "hotellondon@example.com", CostPerNight = 500.00m, Website = "https://hotellondon.com", Description = "Pięciogwiazdkowy hotel w centrum Londynu", Image = "hotellondon.jpg" },
            new Hotel { HotelName = "Hotel Tokio", Address = "1-1-1 Yoyogi, Shibuya City, Tokio 151-8585, Japonia", PostalCode = "151-8585", City = "Tokio", Country = "Japonia", PhoneNumber = "891234567", Email = "hoteltokyo@example.com", CostPerNight = 600.00m, Website = "https://hoteltokyo.com", Description = "Luksusowy hotel w centrum Tokio", Image = "hoteltokyo.jpg" },
            new Hotel { HotelName = "Hotel Sydney", Address = "1 Martin Pl, Sydney NSW 2000, Australia", PostalCode = "2000", City = "Sydney", Country = "Australia", PhoneNumber = "765432189", Email = "hotelsydney@example.com", CostPerNight = 700.00m, Website = "https://hotelsydney.com", Description = "Pięciogwiazdkowy hotel w centrum Sydney", Image = "hotelsydney.jpg" }
            };

            foreach (Hotel h in hotels)
            {
                context.Hotels.Add(h);
            }
            context.SaveChanges();
        }
    }
}
