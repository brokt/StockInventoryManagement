
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
using System.Linq;
using Core.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Handlers.Categories.Queries
{

    public class GetHierarchicalCategoriesQuery : IRequest<IDataResult<IEnumerable<HierarchicalDto>>>
    {
        public int? Id { get; set; }
        public class GetHierarchicalCategoriesQueryHandler : IRequestHandler<GetHierarchicalCategoriesQuery, IDataResult<IEnumerable<HierarchicalDto>>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMediator _mediator;

            public GetHierarchicalCategoriesQueryHandler(ICategoryRepository categoryRepository, IMediator mediator)
            {
                _categoryRepository = categoryRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<HierarchicalDto>>> Handle(GetHierarchicalCategoriesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<HierarchicalDto>>(await _categoryRepository.Query().Include(i => i.ParentCategory).Where(m => request.Id.HasValue ? m.ParentCategoryId == request.Id : m.ParentCategoryId == null).Select(x => new HierarchicalDto(){
                    Id = x.Id,
                    Name = x.Name,
                    HasChildren = _categoryRepository.Query().Any(a => a.ParentCategoryId == x.Id)
                }).ToListAsync());
            }
        }
    }
}