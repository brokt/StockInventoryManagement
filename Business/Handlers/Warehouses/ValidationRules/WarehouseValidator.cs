
using Business.Handlers.Warehouses.Commands;
using FluentValidation;

namespace Business.Handlers.Warehouses.ValidationRules
{

    public class CreateWarehouseValidator : AbstractValidator<CreateWarehouseCommand>
    {
        public CreateWarehouseValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();

        }
    }
    public class UpdateWarehouseValidator : AbstractValidator<UpdateWarehouseCommand>
    {
        public UpdateWarehouseValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();

        }
    }
}