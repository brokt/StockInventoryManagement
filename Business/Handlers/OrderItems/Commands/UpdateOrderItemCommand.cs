
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
using Business.Handlers.OrderItems.ValidationRules;


namespace Business.Handlers.OrderItems.Commands
{


    public class UpdateOrderItemCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, IResult>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMediator _mediator;

            public UpdateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMediator mediator)
            {
                _orderItemRepository = orderItemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrderItemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
            {
                var isThereOrderItemRecord = await _orderItemRepository.GetAsync(u => u.Id == request.Id);


                isThereOrderItemRecord.OrderId = request.OrderId;
                isThereOrderItemRecord.ProductId = request.ProductId;
                isThereOrderItemRecord.Quantity = request.Quantity;
                isThereOrderItemRecord.UnitPrice = request.UnitPrice;


                _orderItemRepository.Update(isThereOrderItemRecord);
                await _orderItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

