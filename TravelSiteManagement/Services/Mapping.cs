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
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
            TypeAdapterConfig<Reservation, ClientOrderViewModel>.NewConfig()
                .Map(dest => dest.ReservationID, src => src.ReservationID)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Cost, src => src.Cost);
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
