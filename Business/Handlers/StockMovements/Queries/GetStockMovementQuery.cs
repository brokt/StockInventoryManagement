
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.StockMovements.Queries
{
    public class GetStockMovementQuery : IRequest<IDataResult<StockMovement>>
    {
        public int Id { get; set; }

        public class GetStockMovementQueryHandler : IRequestHandler<GetStockMovementQuery, IDataResult<StockMovement>>
        {
            private readonly IStockMovementRepository _stockMovementRepository;
            private readonly IMediator _mediator;

            public GetStockMovementQueryHandler(IStockMovementRepository stockMovementRepository, IMediator mediator)
            {
                _stockMovementRepository = stockMovementRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<StockMovement>> Handle(GetStockMovementQuery request, CancellationToken cancellationToken)
            {
                var stockMovement = await _stockMovementRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<StockMovement>(stockMovement);
            }
        }
    }
}
