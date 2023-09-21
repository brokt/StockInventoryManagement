
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.StockMovements.ValidationRules;


namespace Business.Handlers.StockMovements.Commands
{


    public class UpdateStockMovementCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime MovementDate { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        public class UpdateStockMovementCommandHandler : IRequestHandler<UpdateStockMovementCommand, IResult>
        {
            private readonly IStockMovementRepository _stockMovementRepository;
            private readonly IMediator _mediator;

            public UpdateStockMovementCommandHandler(IStockMovementRepository stockMovementRepository, IMediator mediator)
            {
                _stockMovementRepository = stockMovementRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateStockMovementValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateStockMovementCommand request, CancellationToken cancellationToken)
            {
                var isThereStockMovementRecord = await _stockMovementRepository.GetAsync(u => u.Id == request.Id);


                isThereStockMovementRecord.MovementDate = request.MovementDate;
                isThereStockMovementRecord.Quantity = request.Quantity;
                isThereStockMovementRecord.ProductId = request.ProductId;
                isThereStockMovementRecord.WarehouseId = request.WarehouseId;


                _stockMovementRepository.Update(isThereStockMovementRecord);
                await _stockMovementRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

