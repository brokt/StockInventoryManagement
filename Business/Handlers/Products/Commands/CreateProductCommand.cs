
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Products.ValidationRules;

namespace Business.Handlers.Products.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProductCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int CurrentStockQuantity { get; set; }
        public int MinimumStockQuantity { get; set; }
        public int MaximumStockQuantity { get; set; }
        public decimal Weight { get; set; } = 0;
        public string Brand { get; set; }
        public string ShelfNumber { get; set; }
        public string BatchNumber { get; set; }
        public System.Collections.Generic.List<StockMovement> StockMovements { get; set; }
        public System.Collections.Generic.List<ProductCategories> ProductCategories { get; set; }
        public System.Collections.Generic.List<ProductSupplier> ProductSuppliers { get; set; }


        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProductValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var isThereProductRecord = _productRepository.Query().Any(u => u.Name == request.Name);

                if (isThereProductRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProduct = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    SKU = request.SKU,
                    UnitPrice = request.UnitPrice,
                    CostPrice = request.CostPrice,
                    CurrentStockQuantity = request.CurrentStockQuantity,
                    MinimumStockQuantity = request.MinimumStockQuantity,
                    MaximumStockQuantity = request.MaximumStockQuantity,
                    Weight = request.Weight,
                    Brand = request.Brand,
                    ShelfNumber = request.ShelfNumber,
                    BatchNumber = request.BatchNumber,
                    StockMovements = request.StockMovements,
                    ProductCategories = request.ProductCategories,
                    ProductSuppliers = request.ProductSuppliers,

                };

                _productRepository.Add(addedProduct);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}