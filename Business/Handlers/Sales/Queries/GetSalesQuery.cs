
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Sales.Queries
{

    public class GetSalesQuery : IRequest<IDataResult<IEnumerable<Sale>>>
    {
        public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, IDataResult<IEnumerable<Sale>>>
        {
            private readonly ISaleRepository _saleRepository;
            private readonly IMediator _mediator;

            public GetSalesQueryHandler(ISaleRepository saleRepository, IMediator mediator)
            {
                _saleRepository = saleRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Sale>>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Sale>>(await _saleRepository.GetListAsync());
            }
        }
    }
}