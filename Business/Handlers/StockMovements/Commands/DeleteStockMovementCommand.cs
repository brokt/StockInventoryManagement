
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.StockMovements.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteStockMovementCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteStockMovementCommandHandler : IRequestHandler<DeleteStockMovementCommand, IResult>
        {
            private readonly IStockMovementRepository _stockMovementRepository;
            private readonly IMediator _mediator;

            public DeleteStockMovementCommandHandler(IStockMovementRepository stockMovementRepository, IMediator mediator)
            {
                _stockMovementRepository = stockMovementRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteStockMovementCommand request, CancellationToken cancellationToken)
            {
                var stockMovementToDelete = _stockMovementRepository.Get(p => p.Id == request.Id);

                _stockMovementRepository.Delete(stockMovementToDelete);
                await _stockMovementRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

