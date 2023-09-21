
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


namespace Business.Handlers.SaleItems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSaleItemCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteSaleItemCommandHandler : IRequestHandler<DeleteSaleItemCommand, IResult>
        {
            private readonly ISaleItemRepository _saleItemRepository;
            private readonly IMediator _mediator;

            public DeleteSaleItemCommandHandler(ISaleItemRepository saleItemRepository, IMediator mediator)
            {
                _saleItemRepository = saleItemRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
            {
                var saleItemToDelete = _saleItemRepository.Get(p => p.Id == request.Id);

                _saleItemRepository.Delete(saleItemToDelete);
                await _saleItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

