
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
using Business.Handlers.SaleItems.ValidationRules;

namespace Business.Handlers.SaleItems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSaleItemCommand : IRequest<IResult>
    {

        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }


        public class CreateSaleItemCommandHandler : IRequestHandler<CreateSaleItemCommand, IResult>
        {
            private readonly ISaleItemRepository _saleItemRepository;
            private readonly IMediator _mediator;
            public CreateSaleItemCommandHandler(ISaleItemRepository saleItemRepository, IMediator mediator)
            {
                _saleItemRepository = saleItemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSaleItemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSaleItemCommand request, CancellationToken cancellationToken)
            {
                var isThereSaleItemRecord = _saleItemRepository.Query().Any(u => u.SaleId == request.SaleId);

                if (isThereSaleItemRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSaleItem = new SaleItem
                {
                    SaleId = request.SaleId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    UnitPrice = request.UnitPrice,

                };

                _saleItemRepository.Add(addedSaleItem);
                await _saleItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}