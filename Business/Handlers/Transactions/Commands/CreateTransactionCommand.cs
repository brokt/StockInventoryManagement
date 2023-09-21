
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
using Business.Handlers.Transactions.ValidationRules;

namespace Business.Handlers.Transactions.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateTransactionCommand : IRequest<IResult>
    {

        public System.DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public int CustomerAccountId { get; set; }


        public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, IResult>
        {
            private readonly ITransactionRepository _transactionRepository;
            private readonly IMediator _mediator;
            public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IMediator mediator)
            {
                _transactionRepository = transactionRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateTransactionValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
            {
                var isThereTransactionRecord = _transactionRepository.Query().Any(u => u.TransactionDate == request.TransactionDate);

                if (isThereTransactionRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedTransaction = new Transaction
                {
                    TransactionDate = request.TransactionDate,
                    Amount = request.Amount,
                    CustomerAccountId = request.CustomerAccountId,

                };

                _transactionRepository.Add(addedTransaction);
                await _transactionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}