
using Business.Handlers.Transactions.Commands;
using FluentValidation;

namespace Business.Handlers.Transactions.ValidationRules
{

    public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.TransactionDate).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.CustomerAccountId).NotEmpty();

        }
    }
    public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public UpdateTransactionValidator()
        {
            RuleFor(x => x.TransactionDate).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.CustomerAccountId).NotEmpty();

        }
    }
}