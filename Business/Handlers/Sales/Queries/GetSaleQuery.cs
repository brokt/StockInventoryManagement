
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Sales.Queries
{
    public class GetSaleQuery : IRequest<IDataResult<Sale>>
    {
        public int Id { get; set; }

        public class GetSaleQueryHandler : IRequestHandler<GetSaleQuery, IDataResult<Sale>>
        {
            private readonly ISaleRepository _saleRepository;
            private readonly IMediator _mediator;

            public GetSaleQueryHandler(ISaleRepository saleRepository, IMediator mediator)
            {
                _saleRepository = saleRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Sale>> Handle(GetSaleQuery request, CancellationToken cancellationToken)
            {
                var sale = await _saleRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Sale>(sale);
            }
        }
    }
}
