using Cantine.Application.Models;
using FluentValidation;

namespace CantineWebAPI.Validators
{
    public class BudgetValidator: AbstractValidator<AddBudgetDTO>
    {
        public BudgetValidator() {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("ClientId is required.")
                .NotEqual(Guid.Empty).WithMessage("ClientId can not be an empty Guid.");

            RuleFor(b => b.Amount)
                .GreaterThan(0).WithMessage("Amount must be positive.");
        }
    }
}
