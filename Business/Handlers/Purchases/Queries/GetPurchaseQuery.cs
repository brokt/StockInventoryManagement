
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Purchases.Queries
{
    public class GetPurchaseQuery : IRequest<IDataResult<Purchase>>
    {
        public int Id { get; set; }

        public class GetPurchaseQueryHandler : IRequestHandler<GetPurchaseQuery, IDataResult<Purchase>>
        {
            private readonly IPurchaseRepository _purchaseRepository;
            private readonly IMediator _mediator;

            public GetPurchaseQueryHandler(IPurchaseRepository purchaseRepository, IMediator mediator)
            {
                _purchaseRepository = purchaseRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Purchase>> Handle(GetPurchaseQuery request, CancellationToken cancellationToken)
            {
                var purchase = await _purchaseRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Purchase>(purchase);
            }
        }
    }
}
