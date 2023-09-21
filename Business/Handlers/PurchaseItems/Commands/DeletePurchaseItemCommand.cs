
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


namespace Business.Handlers.PurchaseItems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePurchaseItemCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeletePurchaseItemCommandHandler : IRequestHandler<DeletePurchaseItemCommand, IResult>
        {
            private readonly IPurchaseItemRepository _purchaseItemRepository;
            private readonly IMediator _mediator;

            public DeletePurchaseItemCommandHandler(IPurchaseItemRepository purchaseItemRepository, IMediator mediator)
            {
                _purchaseItemRepository = purchaseItemRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePurchaseItemCommand request, CancellationToken cancellationToken)
            {
                var purchaseItemToDelete = _purchaseItemRepository.Get(p => p.Id == request.Id);

                _purchaseItemRepository.Delete(purchaseItemToDelete);
                await _purchaseItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

