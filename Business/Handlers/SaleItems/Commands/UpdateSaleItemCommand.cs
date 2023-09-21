
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
using Business.Handlers.SaleItems.ValidationRules;


namespace Business.Handlers.SaleItems.Commands
{


    public class UpdateSaleItemCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public class UpdateSaleItemCommandHandler : IRequestHandler<UpdateSaleItemCommand, IResult>
        {
            private readonly ISaleItemRepository _saleItemRepository;
            private readonly IMediator _mediator;

            public UpdateSaleItemCommandHandler(ISaleItemRepository saleItemRepository, IMediator mediator)
            {
                _saleItemRepository = saleItemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSaleItemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
            {
                var isThereSaleItemRecord = await _saleItemRepository.GetAsync(u => u.Id == request.Id);


                isThereSaleItemRecord.SaleId = request.SaleId;
                isThereSaleItemRecord.ProductId = request.ProductId;
                isThereSaleItemRecord.Quantity = request.Quantity;
                isThereSaleItemRecord.UnitPrice = request.UnitPrice;


                _saleItemRepository.Update(isThereSaleItemRecord);
                await _saleItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

