
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.CustomerAccounts.Queries
{
    public class GetCustomerAccountQuery : IRequest<IDataResult<CustomerAccount>>
    {
        public int Id { get; set; }

        public class GetCustomerAccountQueryHandler : IRequestHandler<GetCustomerAccountQuery, IDataResult<CustomerAccount>>
        {
            private readonly ICustomerAccountRepository _customerAccountRepository;
            private readonly IMediator _mediator;

            public GetCustomerAccountQueryHandler(ICustomerAccountRepository customerAccountRepository, IMediator mediator)
            {
                _customerAccountRepository = customerAccountRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<CustomerAccount>> Handle(GetCustomerAccountQuery request, CancellationToken cancellationToken)
            {
                var customerAccount = await _customerAccountRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<CustomerAccount>(customerAccount);
            }
        }
    }
}
