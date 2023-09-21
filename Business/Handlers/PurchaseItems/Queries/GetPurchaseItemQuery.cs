
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.PurchaseItems.Queries
{
    public class GetPurchaseItemQuery : IRequest<IDataResult<PurchaseItem>>
    {
        public int Id { get; set; }

        public class GetPurchaseItemQueryHandler : IRequestHandler<GetPurchaseItemQuery, IDataResult<PurchaseItem>>
        {
            private readonly IPurchaseItemRepository _purchaseItemRepository;
            private readonly IMediator _mediator;

            public GetPurchaseItemQueryHandler(IPurchaseItemRepository purchaseItemRepository, IMediator mediator)
            {
                _purchaseItemRepository = purchaseItemRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PurchaseItem>> Handle(GetPurchaseItemQuery request, CancellationToken cancellationToken)
            {
                var purchaseItem = await _purchaseItemRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<PurchaseItem>(purchaseItem);
            }
        }
    }
}
