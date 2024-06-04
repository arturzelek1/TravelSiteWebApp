using FluentValidation;
using TravelSiteWeb.Models;

namespace TravelSiteWeb.Services
{
    public class FlightValidator : AbstractValidator<Flight>
    {
        public FlightValidator()
        {
            RuleFor(x => x.FlightNumber).NotEmpty();
            RuleFor(x => x.DepartureDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(x => x.ArrivalDate).NotEmpty().GreaterThan(d => d.DepartureDate);
            RuleFor(x => x.AirportFrom).NotEmpty();
            RuleFor(x => x.AirportTo).NotEmpty();
            RuleFor(x => x.FlightCost).NotEmpty();
            RuleFor(x => x.AirportCity).NotEmpty();
        }
    }
    
}
