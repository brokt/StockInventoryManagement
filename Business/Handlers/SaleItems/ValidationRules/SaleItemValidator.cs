
using Business.Handlers.SaleItems.Commands;
using FluentValidation;

namespace Business.Handlers.SaleItems.ValidationRules
{

    public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
    {
        public CreateSaleItemValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();

        }
    }
    public class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        public UpdateSaleItemValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();

        }
    }
}