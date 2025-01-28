using Cantine.Application.Models;
using FluentValidation;

namespace CantineWebAPI.Validators
{
    public class TicketRequestValidator: AbstractValidator<TicketRequestDTO>
    {
        public TicketRequestValidator()
        {
            RuleFor(x => x.ClientId)
                .NotNull().NotEmpty().WithMessage("ClientId is required.")
                .NotEqual(Guid.Empty).WithMessage("ClientId can not be an empty Guid.");

            RuleFor(x => x.Products)
                .NotEmpty().WithMessage("The products list can not be empty.");

            RuleForEach(x => x.Products)
                .NotEmpty().WithMessage("Product name can not be empty.");
            
        }
    }
}
