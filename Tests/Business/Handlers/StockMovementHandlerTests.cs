
using Business.Handlers.StockMovements.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.StockMovements.Queries.GetStockMovementQuery;
using Entities.Concrete;
using static Business.Handlers.StockMovements.Queries.GetStockMovementsQuery;
using static Business.Handlers.StockMovements.Commands.CreateStockMovementCommand;
using Business.Handlers.StockMovements.Commands;
using Business.Constants;
using static Business.Handlers.StockMovements.Commands.UpdateStockMovementCommand;
using static Business.Handlers.StockMovements.Commands.DeleteStockMovementCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class StockMovementHandlerTests
    {
        Mock<IStockMovementRepository> _stockMovementRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _stockMovementRepository = new Mock<IStockMovementRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task StockMovement_GetQuery_Success()
        {
            //Arrange
            var query = new GetStockMovementQuery();

            _stockMovementRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<StockMovement, bool>>>())).ReturnsAsync(new StockMovement()
//propertyler buraya yazılacak
//{																		
//StockMovementId = 1,
//StockMovementName = "Test"
//}
);

            var handler = new GetStockMovementQueryHandler(_stockMovementRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.StockMovementId.Should().Be(1);

        }

        [Test]
        public async Task StockMovement_GetQueries_Success()
        {
            //Arrange
            var query = new GetStockMovementsQuery();

            _stockMovementRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<StockMovement, bool>>>()))
                        .ReturnsAsync(new List<StockMovement> { new StockMovement() { /*TODO:propertyler buraya yazılacak StockMovementId = 1, StockMovementName = "test"*/ } });

            var handler = new GetStockMovementsQueryHandler(_stockMovementRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<StockMovement>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task StockMovement_CreateCommand_Success()
        {
            StockMovement rt = null;
            //Arrange
            var command = new CreateStockMovementCommand();
            //propertyler buraya yazılacak
            //command.StockMovementName = "deneme";

            _stockMovementRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<StockMovement, bool>>>()))
                        .ReturnsAsync(rt);

            _stockMovementRepository.Setup(x => x.Add(It.IsAny<StockMovement>())).Returns(new StockMovement());

            var handler = new CreateStockMovementCommandHandler(_stockMovementRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _stockMovementRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task StockMovement_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateStockMovementCommand();
            //propertyler buraya yazılacak 
            //command.StockMovementName = "test";

            _stockMovementRepository.Setup(x => x.Query())
                                           .Returns(new List<StockMovement> { new StockMovement() { /*TODO:propertyler buraya yazılacak StockMovementId = 1, StockMovementName = "test"*/ } }.AsQueryable());

            _stockMovementRepository.Setup(x => x.Add(It.IsAny<StockMovement>())).Returns(new StockMovement());

            var handler = new CreateStockMovementCommandHandler(_stockMovementRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task StockMovement_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateStockMovementCommand();
            //command.StockMovementName = "test";

            _stockMovementRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<StockMovement, bool>>>()))
                        .ReturnsAsync(new StockMovement() { /*TODO:propertyler buraya yazılacak StockMovementId = 1, StockMovementName = "deneme"*/ });

            _stockMovementRepository.Setup(x => x.Update(It.IsAny<StockMovement>())).Returns(new StockMovement());

            var handler = new UpdateStockMovementCommandHandler(_stockMovementRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _stockMovementRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task StockMovement_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteStockMovementCommand();

            _stockMovementRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<StockMovement, bool>>>()))
                        .ReturnsAsync(new StockMovement() { /*TODO:propertyler buraya yazılacak StockMovementId = 1, StockMovementName = "deneme"*/});

            _stockMovementRepository.Setup(x => x.Delete(It.IsAny<StockMovement>()));

            var handler = new DeleteStockMovementCommandHandler(_stockMovementRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _stockMovementRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

