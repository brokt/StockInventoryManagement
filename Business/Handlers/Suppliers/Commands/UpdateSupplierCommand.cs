
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
using Business.Handlers.Suppliers.ValidationRules;


namespace Business.Handlers.Suppliers.Commands
{


    public class UpdateSupplierCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string Address { get; set; }
        public string PaymentTerms { get; set; }
        public System.Collections.Generic.List<ProductSupplier> ProductSuppliers { get; set; }

        public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, IResult>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IMediator _mediator;

            public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository, IMediator mediator)
            {
                _supplierRepository = supplierRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSupplierValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
            {
                var isThereSupplierRecord = await _supplierRepository.GetAsync(u => u.Id == request.Id);


                isThereSupplierRecord.Name = request.Name;
                isThereSupplierRecord.ContactInfo = request.ContactInfo;
                isThereSupplierRecord.Address = request.Address;
                isThereSupplierRecord.PaymentTerms = request.PaymentTerms;
                isThereSupplierRecord.ProductSuppliers = request.ProductSuppliers;


                _supplierRepository.Update(isThereSupplierRecord);
                await _supplierRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

