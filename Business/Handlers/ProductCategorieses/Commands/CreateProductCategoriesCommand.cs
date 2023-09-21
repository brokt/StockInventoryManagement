
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
using Business.Handlers.ProductCategorieses.ValidationRules;

namespace Business.Handlers.ProductCategorieses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProductCategoriesCommand : IRequest<IResult>
    {

        public int ProductId { get; set; }


        public class CreateProductCategoriesCommandHandler : IRequestHandler<CreateProductCategoriesCommand, IResult>
        {
            private readonly IProductCategoriesRepository _productCategoriesRepository;
            private readonly IMediator _mediator;
            public CreateProductCategoriesCommandHandler(IProductCategoriesRepository productCategoriesRepository, IMediator mediator)
            {
                _productCategoriesRepository = productCategoriesRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProductCategoriesValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProductCategoriesCommand request, CancellationToken cancellationToken)
            {
                var isThereProductCategoriesRecord = _productCategoriesRepository.Query().Any(u => u.ProductId == request.ProductId);

                if (isThereProductCategoriesRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProductCategories = new ProductCategories
                {
                    ProductId = request.ProductId,

                };

                _productCategoriesRepository.Add(addedProductCategories);
                await _productCategoriesRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}