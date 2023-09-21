
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

namespace Business.Handlers.OrderItems.Queries
{

    public class GetOrderItemsQuery : IRequest<IDataResult<IEnumerable<OrderItem>>>
    {
        public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, IDataResult<IEnumerable<OrderItem>>>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMediator _mediator;

            public GetOrderItemsQueryHandler(IOrderItemRepository orderItemRepository, IMediator mediator)
            {
                _orderItemRepository = orderItemRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OrderItem>>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OrderItem>>(await _orderItemRepository.GetListAsync());
            }
        }
    }
}