
using Business.Handlers.ProductSuppliers.Commands;
using FluentValidation;

namespace Business.Handlers.ProductSuppliers.ValidationRules
{

    public class CreateProductSupplierValidator : AbstractValidator<CreateProductSupplierCommand>
    {
        public CreateProductSupplierValidator()
        {
            RuleFor(x => x.SupplierId).NotEmpty();

        }
    }
    public class UpdateProductSupplierValidator : AbstractValidator<UpdateProductSupplierCommand>
    {
        public UpdateProductSupplierValidator()
        {
            RuleFor(x => x.SupplierId).NotEmpty();

        }
    }
}