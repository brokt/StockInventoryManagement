
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.CustomerAccounts.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCustomerAccountCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteCustomerAccountCommandHandler : IRequestHandler<DeleteCustomerAccountCommand, IResult>
        {
            private readonly ICustomerAccountRepository _customerAccountRepository;
            private readonly IMediator _mediator;

            public DeleteCustomerAccountCommandHandler(ICustomerAccountRepository customerAccountRepository, IMediator mediator)
            {
                _customerAccountRepository = customerAccountRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteCustomerAccountCommand request, CancellationToken cancellationToken)
            {
                var customerAccountToDelete = _customerAccountRepository.Get(p => p.Id == request.Id);

                _customerAccountRepository.Delete(customerAccountToDelete);
                await _customerAccountRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

