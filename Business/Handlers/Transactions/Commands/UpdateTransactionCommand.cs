
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
using Business.Handlers.Transactions.ValidationRules;


namespace Business.Handlers.Transactions.Commands
{


    public class UpdateTransactionCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public int CustomerAccountId { get; set; }

        public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, IResult>
        {
            private readonly ITransactionRepository _transactionRepository;
            private readonly IMediator _mediator;

            public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMediator mediator)
            {
                _transactionRepository = transactionRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateTransactionValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
            {
                var isThereTransactionRecord = await _transactionRepository.GetAsync(u => u.Id == request.Id);


                isThereTransactionRecord.TransactionDate = request.TransactionDate;
                isThereTransactionRecord.Amount = request.Amount;
                isThereTransactionRecord.CustomerAccountId = request.CustomerAccountId;


                _transactionRepository.Update(isThereTransactionRecord);
                await _transactionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

