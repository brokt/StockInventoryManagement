
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


namespace Business.Handlers.Sales.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSaleCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, IResult>
        {
            private readonly ISaleRepository _saleRepository;
            private readonly IMediator _mediator;

            public DeleteSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
            {
                _saleRepository = saleRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
            {
                var saleToDelete = _saleRepository.Get(p => p.Id == request.Id);

                _saleRepository.Delete(saleToDelete);
                await _saleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

