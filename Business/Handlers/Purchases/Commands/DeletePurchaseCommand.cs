
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


namespace Business.Handlers.Purchases.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePurchaseCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand, IResult>
        {
            private readonly IPurchaseRepository _purchaseRepository;
            private readonly IMediator _mediator;

            public DeletePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IMediator mediator)
            {
                _purchaseRepository = purchaseRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
            {
                var purchaseToDelete = _purchaseRepository.Get(p => p.Id == request.Id);

                _purchaseRepository.Delete(purchaseToDelete);
                await _purchaseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

