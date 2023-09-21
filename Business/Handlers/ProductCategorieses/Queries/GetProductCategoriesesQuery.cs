
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

namespace Business.Handlers.ProductCategorieses.Queries
{

    public class GetProductCategoriesesQuery : IRequest<IDataResult<IEnumerable<ProductCategories>>>
    {
        public class GetProductCategoriesesQueryHandler : IRequestHandler<GetProductCategoriesesQuery, IDataResult<IEnumerable<ProductCategories>>>
        {
            private readonly IProductCategoriesRepository _productCategoriesRepository;
            private readonly IMediator _mediator;

            public GetProductCategoriesesQueryHandler(IProductCategoriesRepository productCategoriesRepository, IMediator mediator)
            {
                _productCategoriesRepository = productCategoriesRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProductCategories>>> Handle(GetProductCategoriesesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProductCategories>>(await _productCategoriesRepository.GetListAsync());
            }
        }
    }
}