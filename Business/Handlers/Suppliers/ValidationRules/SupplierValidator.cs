
using Business.Handlers.Suppliers.Commands;
using FluentValidation;

namespace Business.Handlers.Suppliers.ValidationRules
{

    public class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            //RuleFor(x => x.ContactInfo).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            //RuleFor(x => x.PaymentTerms).NotEmpty();

        }
    }
    public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            //RuleFor(x => x.ContactInfo).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            //RuleFor(x => x.PaymentTerms).NotEmpty();

        }
    }
}