
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
using Business.Handlers.Warehouses.ValidationRules;

namespace Business.Handlers.Warehouses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateWarehouseCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public string Location { get; set; }
        public System.Collections.Generic.List<StockMovement> StockMovements { get; set; }


        public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, IResult>
        {
            private readonly IWarehouseRepository _warehouseRepository;
            private readonly IMediator _mediator;
            public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMediator mediator)
            {
                _warehouseRepository = warehouseRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateWarehouseValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
            {
                var isThereWarehouseRecord = _warehouseRepository.Query().Any(u => u.Name == request.Name);

                if (isThereWarehouseRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedWarehouse = new Warehouse
                {
                    Name = request.Name,
                    Location = request.Location,
                    StockMovements = request.StockMovements,

                };

                _warehouseRepository.Add(addedWarehouse);
                await _warehouseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}