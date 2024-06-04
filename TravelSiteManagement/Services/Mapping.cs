using Mapster;
using TravelSiteWeb.ViewModel;
using TravelSiteWeb.Models;
using TravelSiteWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace TravelSiteWeb.Services
{
    public class MappingService
    {
        public void ConfigureMapping()
        {
            //Maping for ClientViewModel
            TypeAdapterConfig<Client, ClientViewModel>.NewConfig()
                .Map(dest => dest.ClientID, src => src.ClientID)
                //Combine 2 fields for viewmodel
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
            //Mapping for ReservationViewModel
            TypeAdapterConfig<Reservation, ReservationViewModel>.NewConfig()
                .Map(dest => dest.ReservationID, src => src.ReservationID)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Cost, src => src.Cost);
            //Mapping for ClientOrderViewModel
            TypeAdapterConfig<Client, ClientOrderViewModel>.NewConfig()
                .Map(dest => dest.ClientID, src => src.ClientID)
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
            TypeAdapterConfig<Reservation, ClientOrderViewModel>.NewConfig()
                .Map(dest => dest.ReservationID, src => src.ReservationID)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Cost, src => src.Cost);

            // Mapping for OfferViewModel
            TypeAdapterConfig<TravelDestination, Offer>.NewConfig()
                .Map(dest => dest.TravelDestinationID, src => src.TravelDestinationID)
                .Map(dest => dest.DateStart, src => src.DateStart)
                .Map(dest => dest.DateEnd, src => src.DateEnd)
                .Map(dest => dest.FromLocation, src => src.FromLocation)
                .Map(dest => dest.ToLocation, src => src.ToLocation)
                .Map(dest => dest.City, src => src.City);
            TypeAdapterConfig<Hotel, Offer>.NewConfig()
                .Map(dest => dest.HotelID, src => src.HotelID)
                .Map(dest => dest.HotelName, src => src.HotelName)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.HotelCity, src => src.HotelCity)
                .Map(dest => dest.CostPerNight, src => src.CostPerNight)
                .Map(dest => dest.Website, src => src.Website)
                .Map(dest => dest.Image, src => src.Image);
            TypeAdapterConfig<Flight, Offer>.NewConfig()
                .Map(dest => dest.FlightID, src => src.FlightID)
                .Map(dest => dest.FlightNumber, src => src.FlightNumber)
                .Map(dest => dest.DepartureDate, src => src.DepartureDate)
                .Map(dest => dest.ArrivalDate, src => src.ArrivalDate)
                .Map(dest => dest.AirportTo, src => src.AirportTo)
                .Map(dest => dest.AirportFrom, src => src.AirportFrom)
                .Map(dest => dest.FlightCost, src => src.FlightCost);

        }
        public List<ClientOrderViewModel> GetClientOrderViewModels(TravelContext context)
        {
            //Get all clients from db
            var allClients = context.Clients.ToList();
            //Get all reservations from db
            var allReservations = context.Reservations.ToList();



            //Mapping client and reservation data to viewModel
            var clientOrderViewModels = new List<ClientOrderViewModel>();

            foreach (var client in allClients)
            {
                var clientOrderViewModel = client.Adapt<ClientOrderViewModel>();
                var reservationsForClient = allReservations.Where(r => r.ClientID == client.ClientID).ToList();
                clientOrderViewModel.Reservations = reservationsForClient.Adapt<List<ReservationViewModel>>();
                clientOrderViewModels.Add(clientOrderViewModel);
            }
            return clientOrderViewModels;

        }

        public List<Offer> GetOfferViewModel(TravelContext context)
        {
            var allDestinations = context.TravelDestinations.ToList();
            var allHotels = context.Hotels.ToList();
            var allFlights = context.Flights.ToList();

            var offers = new List<Offer>();

            foreach (var destination in allDestinations)
            {
                var offer = destination.Adapt<Offer>();
                offer.Hotels = allHotels
                    .Where(h => h.TravelDestinationID == destination.TravelDestinationID)
                    .Adapt<List<Hotel>>() ?? new List<Hotel>();
                offer.Flights = allFlights
                    .Where(f => f.TravelDestinationID == destination.TravelDestinationID)
                    .Adapt<List<Flight>>() ?? new List<Flight>();
                offers.Add(offer);
            }
            return offers;
        }

        public List<ClientViewModel> GetClientView(TravelContext context)
        {
            var allClients = context.Clients.ToList();
            var clientsViewModels = new List<ClientViewModel>();

            foreach (var client in allClients)
            {
                var clientsViewModel = client.Adapt<ClientViewModel>();
                clientsViewModels.Add(clientsViewModel);
            }
            return clientsViewModels;
        }
    }

}
