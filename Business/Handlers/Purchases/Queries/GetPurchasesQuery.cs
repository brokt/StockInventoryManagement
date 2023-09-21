
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

namespace Business.Handlers.Purchases.Queries
{

    public class GetPurchasesQuery : IRequest<IDataResult<IEnumerable<Purchase>>>
    {
        public class GetPurchasesQueryHandler : IRequestHandler<GetPurchasesQuery, IDataResult<IEnumerable<Purchase>>>
        {
            private readonly IPurchaseRepository _purchaseRepository;
            private readonly IMediator _mediator;

            public GetPurchasesQueryHandler(IPurchaseRepository purchaseRepository, IMediator mediator)
            {
                _purchaseRepository = purchaseRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Purchase>>> Handle(GetPurchasesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Purchase>>(await _purchaseRepository.GetListAsync());
            }
        }
    }
}