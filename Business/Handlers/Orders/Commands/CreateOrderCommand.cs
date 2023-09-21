
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
using Business.Handlers.Orders.ValidationRules;
using Core.Aspects.Autofac.Transaction;

namespace Business.Handlers.Orders.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderCommand : IRequest<IResult>
    {

        public int CustomerId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.Collections.Generic.List<OrderItem> OrderItems { get; set; }


        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, IResult>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMediator _mediator;
            public CreateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator, IOrderItemRepository orderItemRepository)
            {
                _orderRepository = orderRepository;
                _mediator = mediator;
                _orderItemRepository = orderItemRepository;
            }

            [ValidationAspect(typeof(CreateOrderValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            [TransactionScopeAspectAsync]
            public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
              

                var addedOrder = new Order
                {
                    CustomerId = request.CustomerId,
                    OrderDate = request.OrderDate,
                    //OrderItems = request.OrderItems,

                };
                //Add Order
                _orderRepository.Add(addedOrder);                
                await _orderRepository.SaveChangesAsync();

                //Update order Id
                request.OrderItems.ForEach(f => f.OrderId = addedOrder.Id);
                //Add OrderItems
                _orderItemRepository.AddRange(request.OrderItems);
                await _orderItemRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}