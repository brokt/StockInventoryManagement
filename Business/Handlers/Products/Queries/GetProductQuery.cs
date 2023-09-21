
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Business.Handlers.Products.Queries
{
    public class GetProductQuery : IRequest<IDataResult<Product>>
    {
        public int Id { get; set; }

        public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IDataResult<Product>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetProductQueryHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.Query().Include(i => i.ProductCategories).Where(p => p.Id == request.Id).Select(x => new Product()
                {
                    Id = x.Id,
                    Name = x.Name,
                    SKU = x.SKU,
                    ProductCategories = x.ProductCategories,
                    BatchNumber = x.BatchNumber,
                    Brand = x.Brand,
                    CostPrice = x.CostPrice,
                    CurrentStockQuantity = x.CurrentStockQuantity,
                    MinimumStockQuantity = x.MinimumStockQuantity,
                    MaximumStockQuantity = x.MaximumStockQuantity,
                    Description = x.Description,
                    ShelfNumber = x.ShelfNumber,
                    UnitPrice = x.UnitPrice,
                    Weight = x.Weight,
                    
                }).FirstOrDefaultAsync();
                return new SuccessDataResult<Product>(product);
            }
        }
    }
}
