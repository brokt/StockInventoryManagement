
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.SaleItems.Queries
{
    public class GetSaleItemQuery : IRequest<IDataResult<SaleItem>>
    {
        public int Id { get; set; }

        public class GetSaleItemQueryHandler : IRequestHandler<GetSaleItemQuery, IDataResult<SaleItem>>
        {
            private readonly ISaleItemRepository _saleItemRepository;
            private readonly IMediator _mediator;

            public GetSaleItemQueryHandler(ISaleItemRepository saleItemRepository, IMediator mediator)
            {
                _saleItemRepository = saleItemRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<SaleItem>> Handle(GetSaleItemQuery request, CancellationToken cancellationToken)
            {
                var saleItem = await _saleItemRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<SaleItem>(saleItem);
            }
        }
    }
}
