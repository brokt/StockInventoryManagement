
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ProductSuppliers.Queries
{
    public class GetProductSupplierQuery : IRequest<IDataResult<ProductSupplier>>
    {
        public int ProductId { get; set; }

        public class GetProductSupplierQueryHandler : IRequestHandler<GetProductSupplierQuery, IDataResult<ProductSupplier>>
        {
            private readonly IProductSupplierRepository _productSupplierRepository;
            private readonly IMediator _mediator;

            public GetProductSupplierQueryHandler(IProductSupplierRepository productSupplierRepository, IMediator mediator)
            {
                _productSupplierRepository = productSupplierRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProductSupplier>> Handle(GetProductSupplierQuery request, CancellationToken cancellationToken)
            {
                var productSupplier = await _productSupplierRepository.GetAsync(p => p.ProductId == request.ProductId);
                return new SuccessDataResult<ProductSupplier>(productSupplier);
            }
        }
    }
}
