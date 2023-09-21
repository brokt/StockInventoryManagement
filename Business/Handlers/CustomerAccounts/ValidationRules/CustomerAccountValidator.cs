
using Business.Handlers.CustomerAccounts.Commands;
using FluentValidation;

namespace Business.Handlers.CustomerAccounts.ValidationRules
{

    public class CreateCustomerAccountValidator : AbstractValidator<CreateCustomerAccountCommand>
    {
        public CreateCustomerAccountValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.Balance).NotEmpty();

        }
    }
    public class UpdateCustomerAccountValidator : AbstractValidator<UpdateCustomerAccountCommand>
    {
        public UpdateCustomerAccountValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.Balance).NotEmpty();

        }
    }
}