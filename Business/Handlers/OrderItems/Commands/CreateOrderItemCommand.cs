
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
using Business.Handlers.OrderItems.ValidationRules;

namespace Business.Handlers.OrderItems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderItemCommand : IRequest<IResult>
    {

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }


        public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, IResult>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMediator _mediator;
            public CreateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMediator mediator)
            {
                _orderItemRepository = orderItemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrderItemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
            {
                var isThereOrderItemRecord = _orderItemRepository.Query().Any(u => u.OrderId == request.OrderId);

                if (isThereOrderItemRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrderItem = new OrderItem
                {
                    OrderId = request.OrderId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    UnitPrice = request.UnitPrice,

                };

                _orderItemRepository.Add(addedOrderItem);
                await _orderItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}