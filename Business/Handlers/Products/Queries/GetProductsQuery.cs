
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.Entities.Dtos;

namespace Business.Handlers.Products.Queries
{

    public class GetProductsQuery : IRequest<IDataResult<IEnumerable<ProductDto>>>
    {
        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IDataResult<IEnumerable<ProductDto>>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetProductsQueryHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProductDto>>(await _productRepository.Query().Include(i => i.ProductCategories).ThenInclude(i => i.Category).Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CostPrice = x.CostPrice,
                    UnitPrice = x.UnitPrice,
                    CategoriesName = string.Join(",", x.ProductCategories.Select(x => x.Category.Name).ToList())
                }).ToListAsync(cancellationToken));

                //return new SuccessDataResult<IEnumerable<Product>>(await _productRepository.GetListAsync());
            }
        }
    }
}