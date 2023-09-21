
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
using Business.Handlers.ProductSuppliers.ValidationRules;

namespace Business.Handlers.ProductSuppliers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProductSupplierCommand : IRequest<IResult>
    {

        public int SupplierId { get; set; }


        public class CreateProductSupplierCommandHandler : IRequestHandler<CreateProductSupplierCommand, IResult>
        {
            private readonly IProductSupplierRepository _productSupplierRepository;
            private readonly IMediator _mediator;
            public CreateProductSupplierCommandHandler(IProductSupplierRepository productSupplierRepository, IMediator mediator)
            {
                _productSupplierRepository = productSupplierRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProductSupplierValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProductSupplierCommand request, CancellationToken cancellationToken)
            {
                var isThereProductSupplierRecord = _productSupplierRepository.Query().Any(u => u.SupplierId == request.SupplierId);

                if (isThereProductSupplierRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProductSupplier = new ProductSupplier
                {
                    SupplierId = request.SupplierId,

                };

                _productSupplierRepository.Add(addedProductSupplier);
                await _productSupplierRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}