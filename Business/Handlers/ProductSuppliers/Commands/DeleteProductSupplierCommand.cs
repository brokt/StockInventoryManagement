
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.ProductSuppliers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProductSupplierCommand : IRequest<IResult>
    {
        public int ProductId { get; set; }

        public class DeleteProductSupplierCommandHandler : IRequestHandler<DeleteProductSupplierCommand, IResult>
        {
            private readonly IProductSupplierRepository _productSupplierRepository;
            private readonly IMediator _mediator;

            public DeleteProductSupplierCommandHandler(IProductSupplierRepository productSupplierRepository, IMediator mediator)
            {
                _productSupplierRepository = productSupplierRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProductSupplierCommand request, CancellationToken cancellationToken)
            {
                var productSupplierToDelete = _productSupplierRepository.Get(p => p.ProductId == request.ProductId);

                _productSupplierRepository.Delete(productSupplierToDelete);
                await _productSupplierRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

