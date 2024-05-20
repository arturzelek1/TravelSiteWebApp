using FluentValidation;
using TravelSiteWeb.Models;
using RepositoryUsingEFinMVC.Repository;

//Custom Validation for Client

namespace TravelSiteWeb.Services
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Field cannot be empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Field cannot be empty");
            RuleFor(x => x.Address).NotEmpty().Length(5, 255);
            RuleFor(x => x.PhoneNumber).Length(7, 14);
        }
    }
}
