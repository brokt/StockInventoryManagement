
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OrderItems.Queries
{
    public class GetOrderItemQuery : IRequest<IDataResult<OrderItem>>
    {
        public int Id { get; set; }

        public class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, IDataResult<OrderItem>>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMediator _mediator;

            public GetOrderItemQueryHandler(IOrderItemRepository orderItemRepository, IMediator mediator)
            {
                _orderItemRepository = orderItemRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OrderItem>> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
            {
                var orderItem = await _orderItemRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<OrderItem>(orderItem);
            }
        }
    }
}
