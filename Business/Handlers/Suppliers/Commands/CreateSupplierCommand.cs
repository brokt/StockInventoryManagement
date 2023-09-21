
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
using Business.Handlers.Suppliers.ValidationRules;

namespace Business.Handlers.Suppliers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSupplierCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string Address { get; set; }
        public string PaymentTerms { get; set; }
        public System.Collections.Generic.List<ProductSupplier> ProductSuppliers { get; set; }


        public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, IResult>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IMediator _mediator;
            public CreateSupplierCommandHandler(ISupplierRepository supplierRepository, IMediator mediator)
            {
                _supplierRepository = supplierRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSupplierValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
            {
                var isThereSupplierRecord = _supplierRepository.Query().Any(u => u.Name == request.Name);

                if (isThereSupplierRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSupplier = new Supplier
                {
                    Name = request.Name,
                    ContactInfo = request.ContactInfo,
                    Address = request.Address,
                    PaymentTerms = request.PaymentTerms,
                    ProductSuppliers = request.ProductSuppliers,

                };

                _supplierRepository.Add(addedSupplier);
                await _supplierRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}