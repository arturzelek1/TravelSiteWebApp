using FluentValidation;
using TravelSiteWeb.Models;

namespace TravelSiteWeb.Services
{
    public class HotelValidator : AbstractValidator<Hotel>
    {
        public HotelValidator()
        {
            RuleFor(x => x.HotelName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.HotelCity).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.CostPerNight).NotEmpty();
          
        }
    }
}
