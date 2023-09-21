
using Business.Handlers.OrderItems.Commands;
using FluentValidation;

namespace Business.Handlers.OrderItems.ValidationRules
{

    public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();

        }
    }
    public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();

        }
    }
}