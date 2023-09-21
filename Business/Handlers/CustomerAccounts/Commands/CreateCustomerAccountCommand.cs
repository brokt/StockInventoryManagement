
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
using Business.Handlers.CustomerAccounts.ValidationRules;

namespace Business.Handlers.CustomerAccounts.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCustomerAccountCommand : IRequest<IResult>
    {

        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public System.Collections.Generic.List<Transaction> Transactions { get; set; }


        public class CreateCustomerAccountCommandHandler : IRequestHandler<CreateCustomerAccountCommand, IResult>
        {
            private readonly ICustomerAccountRepository _customerAccountRepository;
            private readonly IMediator _mediator;
            public CreateCustomerAccountCommandHandler(ICustomerAccountRepository customerAccountRepository, IMediator mediator)
            {
                _customerAccountRepository = customerAccountRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCustomerAccountValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCustomerAccountCommand request, CancellationToken cancellationToken)
            {
                var isThereCustomerAccountRecord = _customerAccountRepository.Query().Any(u => u.CustomerId == request.CustomerId);

                if (isThereCustomerAccountRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCustomerAccount = new CustomerAccount
                {
                    CustomerId = request.CustomerId,
                    Balance = request.Balance,
                    Transactions = request.Transactions,

                };

                _customerAccountRepository.Add(addedCustomerAccount);
                await _customerAccountRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}