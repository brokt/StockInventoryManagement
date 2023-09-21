
using Business.Handlers.Purchases.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Purchases.Queries.GetPurchaseQuery;
using Entities.Concrete;
using static Business.Handlers.Purchases.Queries.GetPurchasesQuery;
using static Business.Handlers.Purchases.Commands.CreatePurchaseCommand;
using Business.Handlers.Purchases.Commands;
using Business.Constants;
using static Business.Handlers.Purchases.Commands.UpdatePurchaseCommand;
using static Business.Handlers.Purchases.Commands.DeletePurchaseCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PurchaseHandlerTests
    {
        Mock<IPurchaseRepository> _purchaseRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _purchaseRepository = new Mock<IPurchaseRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Purchase_GetQuery_Success()
        {
            //Arrange
            var query = new GetPurchaseQuery();

            _purchaseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Purchase, bool>>>())).ReturnsAsync(new Purchase()
//propertyler buraya yazılacak
//{																		
//PurchaseId = 1,
//PurchaseName = "Test"
//}
);

            var handler = new GetPurchaseQueryHandler(_purchaseRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PurchaseId.Should().Be(1);

        }

        [Test]
        public async Task Purchase_GetQueries_Success()
        {
            //Arrange
            var query = new GetPurchasesQuery();

            _purchaseRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Purchase, bool>>>()))
                        .ReturnsAsync(new List<Purchase> { new Purchase() { /*TODO:propertyler buraya yazılacak PurchaseId = 1, PurchaseName = "test"*/ } });

            var handler = new GetPurchasesQueryHandler(_purchaseRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Purchase>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Purchase_CreateCommand_Success()
        {
            Purchase rt = null;
            //Arrange
            var command = new CreatePurchaseCommand();
            //propertyler buraya yazılacak
            //command.PurchaseName = "deneme";

            _purchaseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Purchase, bool>>>()))
                        .ReturnsAsync(rt);

            _purchaseRepository.Setup(x => x.Add(It.IsAny<Purchase>())).Returns(new Purchase());

            var handler = new CreatePurchaseCommandHandler(_purchaseRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _purchaseRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Purchase_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePurchaseCommand();
            //propertyler buraya yazılacak 
            //command.PurchaseName = "test";

            _purchaseRepository.Setup(x => x.Query())
                                           .Returns(new List<Purchase> { new Purchase() { /*TODO:propertyler buraya yazılacak PurchaseId = 1, PurchaseName = "test"*/ } }.AsQueryable());

            _purchaseRepository.Setup(x => x.Add(It.IsAny<Purchase>())).Returns(new Purchase());

            var handler = new CreatePurchaseCommandHandler(_purchaseRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Purchase_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePurchaseCommand();
            //command.PurchaseName = "test";

            _purchaseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Purchase, bool>>>()))
                        .ReturnsAsync(new Purchase() { /*TODO:propertyler buraya yazılacak PurchaseId = 1, PurchaseName = "deneme"*/ });

            _purchaseRepository.Setup(x => x.Update(It.IsAny<Purchase>())).Returns(new Purchase());

            var handler = new UpdatePurchaseCommandHandler(_purchaseRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _purchaseRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Purchase_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePurchaseCommand();

            _purchaseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Purchase, bool>>>()))
                        .ReturnsAsync(new Purchase() { /*TODO:propertyler buraya yazılacak PurchaseId = 1, PurchaseName = "deneme"*/});

            _purchaseRepository.Setup(x => x.Delete(It.IsAny<Purchase>()));

            var handler = new DeletePurchaseCommandHandler(_purchaseRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _purchaseRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

