
using Business.Handlers.Products.Commands;
using FluentValidation;

namespace Business.Handlers.Products.ValidationRules
{

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.SKU).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();
            RuleFor(x => x.CostPrice).NotEmpty();
            RuleFor(x => x.CurrentStockQuantity).NotEmpty();
            RuleFor(x => x.MinimumStockQuantity).NotEmpty();
            RuleFor(x => x.MaximumStockQuantity).NotEmpty();
            RuleFor(x => x.Weight).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.ShelfNumber).NotEmpty();
            RuleFor(x => x.BatchNumber).NotEmpty();
            //RuleFor(x => x.StockMovements).NotEmpty();
            //RuleFor(x => x.ProductCategories).NotEmpty();
            //RuleFor(x => x.ProductSuppliers).NotEmpty();

        }
    }
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.SKU).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();
            RuleFor(x => x.CostPrice).NotEmpty();
            RuleFor(x => x.CurrentStockQuantity).NotEmpty();
            RuleFor(x => x.MinimumStockQuantity).NotEmpty();
            RuleFor(x => x.MaximumStockQuantity).NotEmpty();
            RuleFor(x => x.Weight).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.ShelfNumber).NotEmpty();
            RuleFor(x => x.BatchNumber).NotEmpty();
            //RuleFor(x => x.StockMovements).NotEmpty();
            //RuleFor(x => x.ProductCategories).NotEmpty();
            //RuleFor(x => x.ProductSuppliers).NotEmpty();

        }
    }
}