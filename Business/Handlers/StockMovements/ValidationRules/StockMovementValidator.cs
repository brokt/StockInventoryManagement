
using Business.Handlers.StockMovements.Commands;
using FluentValidation;

namespace Business.Handlers.StockMovements.ValidationRules
{

    public class CreateStockMovementValidator : AbstractValidator<CreateStockMovementCommand>
    {
        public CreateStockMovementValidator()
        {
            RuleFor(x => x.MovementDate).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.WarehouseId).NotEmpty();

        }
    }
    public class UpdateStockMovementValidator : AbstractValidator<UpdateStockMovementCommand>
    {
        public UpdateStockMovementValidator()
        {
            RuleFor(x => x.MovementDate).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.WarehouseId).NotEmpty();

        }
    }
}