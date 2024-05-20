using FluentValidation;
using TravelSiteWeb.Models;
using RepositoryUsingEFinMVC.Repository;
using System;

//Custom Validation for Client

namespace TravelSiteWeb.Services
{
    public class TravelDestinationValidator : AbstractValidator<TravelDestination>
    {
        public TravelDestinationValidator()
        {
            RuleFor(x => x.Country).NotNull().Must(country => BeValidEnum(country)).WithMessage("Invalid country for trip");
            RuleFor(x => x.Cost).NotNull();
            RuleFor(x => x.City).NotEmpty().Length(20, 255);
        }
        private bool BeValidEnum(Country? country)
        {
            return country.HasValue && Enum.IsDefined(typeof(Country), country.Value);
        }
    }
}
