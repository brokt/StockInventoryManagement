
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

namespace Business.Handlers.SaleItems.Queries
{

    public class GetSaleItemsQuery : IRequest<IDataResult<IEnumerable<SaleItem>>>
    {
        public class GetSaleItemsQueryHandler : IRequestHandler<GetSaleItemsQuery, IDataResult<IEnumerable<SaleItem>>>
        {
            private readonly ISaleItemRepository _saleItemRepository;
            private readonly IMediator _mediator;

            public GetSaleItemsQueryHandler(ISaleItemRepository saleItemRepository, IMediator mediator)
            {
                _saleItemRepository = saleItemRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<SaleItem>>> Handle(GetSaleItemsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SaleItem>>(await _saleItemRepository.GetListAsync());
            }
        }
    }
}