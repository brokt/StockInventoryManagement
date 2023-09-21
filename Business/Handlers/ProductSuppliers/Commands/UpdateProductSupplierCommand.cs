
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
using Business.Handlers.ProductSuppliers.ValidationRules;


namespace Business.Handlers.ProductSuppliers.Commands
{


    public class UpdateProductSupplierCommand : IRequest<IResult>
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }

        public class UpdateProductSupplierCommandHandler : IRequestHandler<UpdateProductSupplierCommand, IResult>
        {
            private readonly IProductSupplierRepository _productSupplierRepository;
            private readonly IMediator _mediator;

            public UpdateProductSupplierCommandHandler(IProductSupplierRepository productSupplierRepository, IMediator mediator)
            {
                _productSupplierRepository = productSupplierRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProductSupplierValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProductSupplierCommand request, CancellationToken cancellationToken)
            {
                var isThereProductSupplierRecord = await _productSupplierRepository.GetAsync(u => u.ProductId == request.ProductId);


                isThereProductSupplierRecord.SupplierId = request.SupplierId;


                _productSupplierRepository.Update(isThereProductSupplierRecord);
                await _productSupplierRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

