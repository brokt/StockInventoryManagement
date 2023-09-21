
using Business.Handlers.Sales.Commands;
using FluentValidation;

namespace Business.Handlers.Sales.ValidationRules
{

    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.SaleDate).NotEmpty();
            RuleFor(x => x.SaleItems).NotEmpty();

        }
    }
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.SaleDate).NotEmpty();
            RuleFor(x => x.SaleItems).NotEmpty();

        }
    }
}