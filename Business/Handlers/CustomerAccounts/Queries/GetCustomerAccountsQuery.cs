
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

namespace Business.Handlers.CustomerAccounts.Queries
{

    public class GetCustomerAccountsQuery : IRequest<IDataResult<IEnumerable<CustomerAccount>>>
    {
        public class GetCustomerAccountsQueryHandler : IRequestHandler<GetCustomerAccountsQuery, IDataResult<IEnumerable<CustomerAccount>>>
        {
            private readonly ICustomerAccountRepository _customerAccountRepository;
            private readonly IMediator _mediator;

            public GetCustomerAccountsQueryHandler(ICustomerAccountRepository customerAccountRepository, IMediator mediator)
            {
                _customerAccountRepository = customerAccountRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<CustomerAccount>>> Handle(GetCustomerAccountsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<CustomerAccount>>(await _customerAccountRepository.GetListAsync());
            }
        }
    }
}