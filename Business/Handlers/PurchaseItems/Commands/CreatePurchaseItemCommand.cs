
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
using Business.Handlers.PurchaseItems.ValidationRules;

namespace Business.Handlers.PurchaseItems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePurchaseItemCommand : IRequest<IResult>
    {

        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }


        public class CreatePurchaseItemCommandHandler : IRequestHandler<CreatePurchaseItemCommand, IResult>
        {
            private readonly IPurchaseItemRepository _purchaseItemRepository;
            private readonly IMediator _mediator;
            public CreatePurchaseItemCommandHandler(IPurchaseItemRepository purchaseItemRepository, IMediator mediator)
            {
                _purchaseItemRepository = purchaseItemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePurchaseItemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePurchaseItemCommand request, CancellationToken cancellationToken)
            {
                var isTherePurchaseItemRecord = _purchaseItemRepository.Query().Any(u => u.PurchaseId == request.PurchaseId);

                if (isTherePurchaseItemRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPurchaseItem = new PurchaseItem
                {
                    PurchaseId = request.PurchaseId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    UnitPrice = request.UnitPrice,

                };

                _purchaseItemRepository.Add(addedPurchaseItem);
                await _purchaseItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}