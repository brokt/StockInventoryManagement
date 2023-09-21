
using Business.Handlers.Purchases.Commands;
using FluentValidation;

namespace Business.Handlers.Purchases.ValidationRules
{

    public class CreatePurchaseValidator : AbstractValidator<CreatePurchaseCommand>
    {
        public CreatePurchaseValidator()
        {
            RuleFor(x => x.SupplierId).NotEmpty();
            RuleFor(x => x.PurchaseDate).NotEmpty();
            RuleFor(x => x.PurchaseItems).NotEmpty();

        }
    }
    public class UpdatePurchaseValidator : AbstractValidator<UpdatePurchaseCommand>
    {
        public UpdatePurchaseValidator()
        {
            RuleFor(x => x.SupplierId).NotEmpty();
            RuleFor(x => x.PurchaseDate).NotEmpty();
            RuleFor(x => x.PurchaseItems).NotEmpty();

        }
    }
}