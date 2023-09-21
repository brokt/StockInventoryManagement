
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


namespace Business.Handlers.ProductCategorieses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProductCategoriesCommand : IRequest<IResult>
    {
        public int CategoryId { get; set; }

        public class DeleteProductCategoriesCommandHandler : IRequestHandler<DeleteProductCategoriesCommand, IResult>
        {
            private readonly IProductCategoriesRepository _productCategoriesRepository;
            private readonly IMediator _mediator;

            public DeleteProductCategoriesCommandHandler(IProductCategoriesRepository productCategoriesRepository, IMediator mediator)
            {
                _productCategoriesRepository = productCategoriesRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProductCategoriesCommand request, CancellationToken cancellationToken)
            {
                var productCategoriesToDelete = _productCategoriesRepository.Get(p => p.CategoryId == request.CategoryId);

                _productCategoriesRepository.Delete(productCategoriesToDelete);
                await _productCategoriesRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

