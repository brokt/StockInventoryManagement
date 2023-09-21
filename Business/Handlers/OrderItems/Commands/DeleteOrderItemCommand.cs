
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


namespace Business.Handlers.OrderItems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrderItemCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, IResult>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMediator _mediator;

            public DeleteOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMediator mediator)
            {
                _orderItemRepository = orderItemRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
            {
                var orderItemToDelete = _orderItemRepository.Get(p => p.Id == request.Id);

                _orderItemRepository.Delete(orderItemToDelete);
                await _orderItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

