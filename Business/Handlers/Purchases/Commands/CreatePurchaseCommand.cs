
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
using Business.Handlers.Purchases.ValidationRules;

namespace Business.Handlers.Purchases.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePurchaseCommand : IRequest<IResult>
    {

        public int SupplierId { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public System.Collections.Generic.List<PurchaseItem> PurchaseItems { get; set; }


        public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, IResult>
        {
            private readonly IPurchaseRepository _purchaseRepository;
            private readonly IMediator _mediator;
            public CreatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IMediator mediator)
            {
                _purchaseRepository = purchaseRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePurchaseValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
            {
                var isTherePurchaseRecord = _purchaseRepository.Query().Any(u => u.SupplierId == request.SupplierId);

                if (isTherePurchaseRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPurchase = new Purchase
                {
                    SupplierId = request.SupplierId,
                    PurchaseDate = request.PurchaseDate,
                    PurchaseItems = request.PurchaseItems,

                };

                _purchaseRepository.Add(addedPurchase);
                await _purchaseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}