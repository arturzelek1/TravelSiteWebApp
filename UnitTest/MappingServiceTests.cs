using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mapster;
using TravelSiteWeb.Models;
using TravelSiteWeb.ViewModel;
using TravelSiteWeb.Services;
using Xunit;
using TravelSiteWeb.Data;

namespace TravelSiteWeb.Tests
{
    public class MappingServiceTests
    {
        private readonly TravelContext _context;
        private readonly MappingService _mappingService;

        public MappingServiceTests()
        {
            var options = new DbContextOptionsBuilder<TravelContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new TravelContext(options);
            _mappingService = new MappingService();
            _mappingService.ConfigureMapping(); // Ensure mappings are configured

            SeedDatabase(); // Seed the database with test data
        }

        private void SeedDatabase()
        {
            // Clear existing data to avoid conflicts
            _context.Clients.RemoveRange(_context.Clients);
            _context.Reservations.RemoveRange(_context.Reservations);
            _context.TravelDestinations.RemoveRange(_context.TravelDestinations);
            _context.Hotels.RemoveRange(_context.Hotels);
            _context.Flights.RemoveRange(_context.Flights);
            _context.SaveChanges();

            // Add test data for one client
            _context.Clients.AddRange(
                new Client
                {
                    ClientID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Address = "123 Elm St",
                    City = "Springfield",
                    Nationality = "American",
                    PostalCode = "12345",
                    Email = "john@example.com",
                    PhoneNumber = "123456789"
                }
            );

            // Add reservations related to the client
            _context.Reservations.AddRange(
                new Reservation
                {
                    ReservationID = 1,
                    ClientID = 1,
                    Title = "Vacation",
                    Cost = 1000
                },
                new Reservation
                {
                    ReservationID = 2,
                    ClientID = 1,
                    Title = "Business Trip",
                    Cost = 500
                }
            );

            // Optionally, add other test data for offers
            _context.TravelDestinations.AddRange(
                new TravelDestination
                {
                    TravelDestinationID = 1,
                    DateStart = DateTime.Now,
                    DateEnd = DateTime.Now.AddDays(5),
                    FromLocation = "NY",
                    ToLocation = "LA",
                    City = "Los Angeles"
                }
            );

            _context.Hotels.AddRange(
                new Hotel
                {
                    HotelID = 1,
                    TravelDestinationID = 1,
                    HotelName = "Grand Hotel",
                    Address = "123 Main St",
                    HotelCity = "Los Angeles",
                    CostPerNight = 200,
                    Website = "grandhotel.com",
                    Image = "hotel.jpg",
                    Email = "contact@grandhotel.com",
                    PhoneNumber = "555-1234",
                    PostalCode = "90001"
                }
            );

            _context.Flights.AddRange(
                new Flight
                {
                    FlightID = 1,
                    TravelDestinationID = 1,
                    FlightNumber = "AA123",
                    DepartureDate = DateTime.Now,
                    ArrivalDate = DateTime.Now.AddHours(5),
                    AirportTo = "LAX",
                    AirportFrom = "JFK",
                    FlightCost = 300,
                    AirportCity = "Los Angeles"
                }
            );

            _context.SaveChanges();
        }

        [Fact]
        public void GetClientOrderViewModels_ReturnsMappedClientOrderViewModels()
        {
            // Act
            var result = _mappingService.GetClientOrderViewModels(_context);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Expecting only one ClientOrderViewModel

            var firstClientOrder = result.First();
            Assert.Equal(1, firstClientOrder.ClientID);
            Assert.Equal("John Doe", firstClientOrder.FullName);
            Assert.Equal(2, firstClientOrder.Reservations.Count); // Expecting two reservations
        }

        [Fact]
        public void GetOfferViewModel_ReturnsMappedOffers()
        {
            // Act
            var result = _mappingService.GetOfferViewModel(_context);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Expecting only one offer

            var offer = result.First();
            Assert.Equal(1, offer.TravelDestinationID);
            Assert.Single(offer.Hotels); // Expecting one hotel

            // Check if the Hotel fields are correctly mapped
            var hotel = offer.Hotels.First();
            Assert.Equal("contact@grandhotel.com", hotel.Email);
            Assert.Equal("555-1234", hotel.PhoneNumber);
            Assert.Equal("90001", hotel.PostalCode);

            Assert.Single(offer.Flights); // Expecting one flight

            // Check if the Flight fields are correctly mapped
            var flight = offer.Flights.First();
            Assert.Equal("Los Angeles", flight.AirportCity);
        }

        [Fact]
        public void GetClientView_ReturnsMappedClientViewModels()
        {
            // Act
            var result = _mappingService.GetClientView(_context);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count); // Expecting one client

            var clientViewModel = result.First();
            Assert.Equal(1, clientViewModel.ClientID);
            Assert.Equal("John Doe", clientViewModel.FullName);
        }
    }
}
