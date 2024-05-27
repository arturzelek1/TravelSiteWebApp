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
            RuleFor(x => x.DateStart).NotNull().GreaterThan(DateTime.Now);
            RuleFor(x => x.DateEnd).NotNull().GreaterThan(DateTime.Now);
            RuleFor(x => x.FromLocation).NotEmpty();
            RuleFor(x => x.ToLocation).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
        }
    }
}
