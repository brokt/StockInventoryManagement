
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Products.ValidationRules;


namespace Business.Handlers.Products.Commands
{


    public class UpdateProductCommand : IRequest<IResult>
    {
        public int Id { get; set; }
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

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public UpdateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProductValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var isThereProductRecord = await _productRepository.GetAsync(u => u.Id == request.Id);


                isThereProductRecord.Name = request.Name;
                isThereProductRecord.Description = request.Description;
                isThereProductRecord.SKU = request.SKU;
                isThereProductRecord.UnitPrice = request.UnitPrice;
                isThereProductRecord.CostPrice = request.CostPrice;
                isThereProductRecord.CurrentStockQuantity = request.CurrentStockQuantity;
                isThereProductRecord.MinimumStockQuantity = request.MinimumStockQuantity;
                isThereProductRecord.MaximumStockQuantity = request.MaximumStockQuantity;
                isThereProductRecord.Weight = request.Weight;
                isThereProductRecord.Brand = request.Brand;
                isThereProductRecord.ShelfNumber = request.ShelfNumber;
                isThereProductRecord.BatchNumber = request.BatchNumber;
                isThereProductRecord.StockMovements = request.StockMovements;
                isThereProductRecord.ProductCategories = request.ProductCategories;
                isThereProductRecord.ProductSuppliers = request.ProductSuppliers;


                _productRepository.Update(isThereProductRecord);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

