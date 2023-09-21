
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
using Business.Handlers.Purchases.ValidationRules;


namespace Business.Handlers.Purchases.Commands
{


    public class UpdatePurchaseCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public System.Collections.Generic.List<PurchaseItem> PurchaseItems { get; set; }

        public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand, IResult>
        {
            private readonly IPurchaseRepository _purchaseRepository;
            private readonly IMediator _mediator;

            public UpdatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IMediator mediator)
            {
                _purchaseRepository = purchaseRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePurchaseValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
            {
                var isTherePurchaseRecord = await _purchaseRepository.GetAsync(u => u.Id == request.Id);


                isTherePurchaseRecord.SupplierId = request.SupplierId;
                isTherePurchaseRecord.PurchaseDate = request.PurchaseDate;
                isTherePurchaseRecord.PurchaseItems = request.PurchaseItems;


                _purchaseRepository.Update(isTherePurchaseRecord);
                await _purchaseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

