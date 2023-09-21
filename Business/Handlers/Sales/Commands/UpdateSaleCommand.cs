
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
using Business.Handlers.Sales.ValidationRules;


namespace Business.Handlers.Sales.Commands
{


    public class UpdateSaleCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime SaleDate { get; set; }
        public System.Collections.Generic.List<SaleItem> SaleItems { get; set; }

        public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, IResult>
        {
            private readonly ISaleRepository _saleRepository;
            private readonly IMediator _mediator;

            public UpdateSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
            {
                _saleRepository = saleRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSaleValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
            {
                var isThereSaleRecord = await _saleRepository.GetAsync(u => u.Id == request.Id);


                isThereSaleRecord.CustomerId = request.CustomerId;
                isThereSaleRecord.SaleDate = request.SaleDate;
                isThereSaleRecord.SaleItems = request.SaleItems;


                _saleRepository.Update(isThereSaleRecord);
                await _saleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

