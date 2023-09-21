
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
using Business.Handlers.ProductCategorieses.ValidationRules;


namespace Business.Handlers.ProductCategorieses.Commands
{


    public class UpdateProductCategoriesCommand : IRequest<IResult>
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public class UpdateProductCategoriesCommandHandler : IRequestHandler<UpdateProductCategoriesCommand, IResult>
        {
            private readonly IProductCategoriesRepository _productCategoriesRepository;
            private readonly IMediator _mediator;

            public UpdateProductCategoriesCommandHandler(IProductCategoriesRepository productCategoriesRepository, IMediator mediator)
            {
                _productCategoriesRepository = productCategoriesRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProductCategoriesValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProductCategoriesCommand request, CancellationToken cancellationToken)
            {
                var isThereProductCategoriesRecord = await _productCategoriesRepository.GetAsync(u => u.CategoryId == request.CategoryId);


                isThereProductCategoriesRecord.ProductId = request.ProductId;


                _productCategoriesRepository.Update(isThereProductCategoriesRecord);
                await _productCategoriesRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

