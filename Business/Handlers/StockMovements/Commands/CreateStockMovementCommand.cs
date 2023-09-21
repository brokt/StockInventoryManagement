
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.StockMovements.ValidationRules;

namespace Business.Handlers.StockMovements.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateStockMovementCommand : IRequest<IResult>
    {

        public System.DateTime MovementDate { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }


        public class CreateStockMovementCommandHandler : IRequestHandler<CreateStockMovementCommand, IResult>
        {
            private readonly IStockMovementRepository _stockMovementRepository;
            private readonly IMediator _mediator;
            public CreateStockMovementCommandHandler(IStockMovementRepository stockMovementRepository, IMediator mediator)
            {
                _stockMovementRepository = stockMovementRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateStockMovementValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateStockMovementCommand request, CancellationToken cancellationToken)
            {
                var isThereStockMovementRecord = _stockMovementRepository.Query().Any(u => u.MovementDate == request.MovementDate);

                if (isThereStockMovementRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedStockMovement = new StockMovement
                {
                    MovementDate = request.MovementDate,
                    Quantity = request.Quantity,
                    ProductId = request.ProductId,
                    WarehouseId = request.WarehouseId,

                };

                _stockMovementRepository.Add(addedStockMovement);
                await _stockMovementRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}