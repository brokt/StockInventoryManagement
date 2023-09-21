
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
using Business.Handlers.Sales.ValidationRules;

namespace Business.Handlers.Sales.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSaleCommand : IRequest<IResult>
    {

        public int CustomerId { get; set; }
        public System.DateTime SaleDate { get; set; }
        public System.Collections.Generic.List<SaleItem> SaleItems { get; set; }


        public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, IResult>
        {
            private readonly ISaleRepository _saleRepository;
            private readonly IMediator _mediator;
            public CreateSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
            {
                _saleRepository = saleRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSaleValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
            {
                var isThereSaleRecord = _saleRepository.Query().Any(u => u.CustomerId == request.CustomerId);

                if (isThereSaleRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSale = new Sale
                {
                    CustomerId = request.CustomerId,
                    SaleDate = request.SaleDate,
                    SaleItems = request.SaleItems,

                };

                _saleRepository.Add(addedSale);
                await _saleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}