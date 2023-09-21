
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ProductCategorieses.Queries
{
    public class GetProductCategoriesQuery : IRequest<IDataResult<ProductCategories>>
    {
        public int CategoryId { get; set; }

        public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, IDataResult<ProductCategories>>
        {
            private readonly IProductCategoriesRepository _productCategoriesRepository;
            private readonly IMediator _mediator;

            public GetProductCategoriesQueryHandler(IProductCategoriesRepository productCategoriesRepository, IMediator mediator)
            {
                _productCategoriesRepository = productCategoriesRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProductCategories>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
            {
                var productCategories = await _productCategoriesRepository.GetAsync(p => p.CategoryId == request.CategoryId);
                return new SuccessDataResult<ProductCategories>(productCategories);
            }
        }
    }
}
