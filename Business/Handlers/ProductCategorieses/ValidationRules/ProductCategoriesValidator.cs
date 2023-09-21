
using Business.Handlers.ProductCategorieses.Commands;
using FluentValidation;

namespace Business.Handlers.ProductCategorieses.ValidationRules
{

    public class CreateProductCategoriesValidator : AbstractValidator<CreateProductCategoriesCommand>
    {
        public CreateProductCategoriesValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();

        }
    }
    public class UpdateProductCategoriesValidator : AbstractValidator<UpdateProductCategoriesCommand>
    {
        public UpdateProductCategoriesValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();

        }
    }
}