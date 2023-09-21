
using Business.Handlers.PurchaseItems.Commands;
using FluentValidation;

namespace Business.Handlers.PurchaseItems.ValidationRules
{

    public class CreatePurchaseItemValidator : AbstractValidator<CreatePurchaseItemCommand>
    {
        public CreatePurchaseItemValidator()
        {
            RuleFor(x => x.PurchaseId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();

        }
    }
    public class UpdatePurchaseItemValidator : AbstractValidator<UpdatePurchaseItemCommand>
    {
        public UpdatePurchaseItemValidator()
        {
            RuleFor(x => x.PurchaseId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();

        }
    }
}