
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

namespace Business.Handlers.PurchaseItems.Queries
{

    public class GetPurchaseItemsQuery : IRequest<IDataResult<IEnumerable<PurchaseItem>>>
    {
        public class GetPurchaseItemsQueryHandler : IRequestHandler<GetPurchaseItemsQuery, IDataResult<IEnumerable<PurchaseItem>>>
        {
            private readonly IPurchaseItemRepository _purchaseItemRepository;
            private readonly IMediator _mediator;

            public GetPurchaseItemsQueryHandler(IPurchaseItemRepository purchaseItemRepository, IMediator mediator)
            {
                _purchaseItemRepository = purchaseItemRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PurchaseItem>>> Handle(GetPurchaseItemsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PurchaseItem>>(await _purchaseItemRepository.GetListAsync());
            }
        }
    }
}