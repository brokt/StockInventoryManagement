
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.ProductSuppliers.Queries
{

    public class GetProductSuppliersQuery : IRequest<IDataResult<IEnumerable<ProductSupplier>>>
    {
        public class GetProductSuppliersQueryHandler : IRequestHandler<GetProductSuppliersQuery, IDataResult<IEnumerable<ProductSupplier>>>
        {
            private readonly IProductSupplierRepository _productSupplierRepository;
            private readonly IMediator _mediator;

            public GetProductSuppliersQueryHandler(IProductSupplierRepository productSupplierRepository, IMediator mediator)
            {
                _productSupplierRepository = productSupplierRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProductSupplier>>> Handle(GetProductSuppliersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProductSupplier>>(await _productSupplierRepository.GetListAsync());
            }
        }
    }
}