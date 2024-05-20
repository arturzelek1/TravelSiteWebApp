using FluentValidation;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb.Models;

namespace TravelSiteWeb.Services
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationValidator(IClientRepository clientRepository, IReservationRepository reservationRepository)
        {
            _clientRepository = clientRepository;
            _reservationRepository = reservationRepository;

            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Cost).NotNull();
            RuleFor(x => x.Description).Length(0, 255);
            RuleFor(x => x.ClientID).Must(BeExistingClientID).WithMessage("Provided ClientID doesn't exist");
            RuleFor(x => x.ClientID).Must(BeExistingTravelDestinationID).WithMessage("Provided TravelDestinationID doesn't exist");
        }

        // Should be private, public for testing
        public bool BeExistingClientID(int clientID)
        {
            var existingClientID = _clientRepository.GetById(clientID);
            return existingClientID != null;
        }

        public bool BeExistingTravelDestinationID(int travelDestinationID)
        {
            var existingTravelDestinationID = _reservationRepository.GetById(travelDestinationID);
            return existingTravelDestinationID != null;
        }
    }
}
