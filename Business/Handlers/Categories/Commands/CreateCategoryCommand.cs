
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
using Business.Handlers.Categories.ValidationRules;

namespace Business.Handlers.Categories.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCategoryCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public System.Collections.Generic.List<ProductCategories> ProductCategories { get; set; }


        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, IResult>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMediator _mediator;
            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMediator mediator)
            {
                _categoryRepository = categoryRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCategoryValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var isThereCategoryRecord = _categoryRepository.Query().Any(u => u.Name == request.Name);

                if (isThereCategoryRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCategory = new Category
                {
                    Name = request.Name,
                    ParentCategoryId = request.ParentCategoryId,
                    ProductCategories = request.ProductCategories,

                };

                _categoryRepository.Add(addedCategory);
                await _categoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}