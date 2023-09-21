
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
using Business.Handlers.CustomerAccounts.ValidationRules;


namespace Business.Handlers.CustomerAccounts.Commands
{


    public class UpdateCustomerAccountCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public System.Collections.Generic.List<Transaction> Transactions { get; set; }

        public class UpdateCustomerAccountCommandHandler : IRequestHandler<UpdateCustomerAccountCommand, IResult>
        {
            private readonly ICustomerAccountRepository _customerAccountRepository;
            private readonly IMediator _mediator;

            public UpdateCustomerAccountCommandHandler(ICustomerAccountRepository customerAccountRepository, IMediator mediator)
            {
                _customerAccountRepository = customerAccountRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCustomerAccountValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCustomerAccountCommand request, CancellationToken cancellationToken)
            {
                var isThereCustomerAccountRecord = await _customerAccountRepository.GetAsync(u => u.Id == request.Id);


                isThereCustomerAccountRecord.CustomerId = request.CustomerId;
                isThereCustomerAccountRecord.Balance = request.Balance;
                isThereCustomerAccountRecord.Transactions = request.Transactions;


                _customerAccountRepository.Update(isThereCustomerAccountRecord);
                await _customerAccountRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

