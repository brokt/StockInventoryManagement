
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

namespace Business.Handlers.StockMovements.Queries
{

    public class GetStockMovementsQuery : IRequest<IDataResult<IEnumerable<StockMovement>>>
    {
        public class GetStockMovementsQueryHandler : IRequestHandler<GetStockMovementsQuery, IDataResult<IEnumerable<StockMovement>>>
        {
            private readonly IStockMovementRepository _stockMovementRepository;
            private readonly IMediator _mediator;

            public GetStockMovementsQueryHandler(IStockMovementRepository stockMovementRepository, IMediator mediator)
            {
                _stockMovementRepository = stockMovementRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<StockMovement>>> Handle(GetStockMovementsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<StockMovement>>(await _stockMovementRepository.GetListAsync());
            }
        }
    }
}