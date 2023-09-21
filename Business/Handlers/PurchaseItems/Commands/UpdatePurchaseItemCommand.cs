
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.PurchaseItems.ValidationRules;


namespace Business.Handlers.PurchaseItems.Commands
{


    public class UpdatePurchaseItemCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public class UpdatePurchaseItemCommandHandler : IRequestHandler<UpdatePurchaseItemCommand, IResult>
        {
            private readonly IPurchaseItemRepository _purchaseItemRepository;
            private readonly IMediator _mediator;

            public UpdatePurchaseItemCommandHandler(IPurchaseItemRepository purchaseItemRepository, IMediator mediator)
            {
                _purchaseItemRepository = purchaseItemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePurchaseItemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePurchaseItemCommand request, CancellationToken cancellationToken)
            {
                var isTherePurchaseItemRecord = await _purchaseItemRepository.GetAsync(u => u.Id == request.Id);


                isTherePurchaseItemRecord.PurchaseId = request.PurchaseId;
                isTherePurchaseItemRecord.ProductId = request.ProductId;
                isTherePurchaseItemRecord.Quantity = request.Quantity;
                isTherePurchaseItemRecord.UnitPrice = request.UnitPrice;


                _purchaseItemRepository.Update(isTherePurchaseItemRecord);
                await _purchaseItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

