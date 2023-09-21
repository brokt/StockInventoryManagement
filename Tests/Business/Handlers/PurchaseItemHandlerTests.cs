
using Business.Handlers.PurchaseItems.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.PurchaseItems.Queries.GetPurchaseItemQuery;
using Entities.Concrete;
using static Business.Handlers.PurchaseItems.Queries.GetPurchaseItemsQuery;
using static Business.Handlers.PurchaseItems.Commands.CreatePurchaseItemCommand;
using Business.Handlers.PurchaseItems.Commands;
using Business.Constants;
using static Business.Handlers.PurchaseItems.Commands.UpdatePurchaseItemCommand;
using static Business.Handlers.PurchaseItems.Commands.DeletePurchaseItemCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PurchaseItemHandlerTests
    {
        Mock<IPurchaseItemRepository> _purchaseItemRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _purchaseItemRepository = new Mock<IPurchaseItemRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task PurchaseItem_GetQuery_Success()
        {
            //Arrange
            var query = new GetPurchaseItemQuery();

            _purchaseItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PurchaseItem, bool>>>())).ReturnsAsync(new PurchaseItem()
//propertyler buraya yazılacak
//{																		
//PurchaseItemId = 1,
//PurchaseItemName = "Test"
//}
);

            var handler = new GetPurchaseItemQueryHandler(_purchaseItemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PurchaseItemId.Should().Be(1);

        }

        [Test]
        public async Task PurchaseItem_GetQueries_Success()
        {
            //Arrange
            var query = new GetPurchaseItemsQuery();

            _purchaseItemRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<PurchaseItem, bool>>>()))
                        .ReturnsAsync(new List<PurchaseItem> { new PurchaseItem() { /*TODO:propertyler buraya yazılacak PurchaseItemId = 1, PurchaseItemName = "test"*/ } });

            var handler = new GetPurchaseItemsQueryHandler(_purchaseItemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<PurchaseItem>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task PurchaseItem_CreateCommand_Success()
        {
            PurchaseItem rt = null;
            //Arrange
            var command = new CreatePurchaseItemCommand();
            //propertyler buraya yazılacak
            //command.PurchaseItemName = "deneme";

            _purchaseItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PurchaseItem, bool>>>()))
                        .ReturnsAsync(rt);

            _purchaseItemRepository.Setup(x => x.Add(It.IsAny<PurchaseItem>())).Returns(new PurchaseItem());

            var handler = new CreatePurchaseItemCommandHandler(_purchaseItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _purchaseItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task PurchaseItem_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePurchaseItemCommand();
            //propertyler buraya yazılacak 
            //command.PurchaseItemName = "test";

            _purchaseItemRepository.Setup(x => x.Query())
                                           .Returns(new List<PurchaseItem> { new PurchaseItem() { /*TODO:propertyler buraya yazılacak PurchaseItemId = 1, PurchaseItemName = "test"*/ } }.AsQueryable());

            _purchaseItemRepository.Setup(x => x.Add(It.IsAny<PurchaseItem>())).Returns(new PurchaseItem());

            var handler = new CreatePurchaseItemCommandHandler(_purchaseItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task PurchaseItem_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePurchaseItemCommand();
            //command.PurchaseItemName = "test";

            _purchaseItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PurchaseItem, bool>>>()))
                        .ReturnsAsync(new PurchaseItem() { /*TODO:propertyler buraya yazılacak PurchaseItemId = 1, PurchaseItemName = "deneme"*/ });

            _purchaseItemRepository.Setup(x => x.Update(It.IsAny<PurchaseItem>())).Returns(new PurchaseItem());

            var handler = new UpdatePurchaseItemCommandHandler(_purchaseItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _purchaseItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task PurchaseItem_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePurchaseItemCommand();

            _purchaseItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PurchaseItem, bool>>>()))
                        .ReturnsAsync(new PurchaseItem() { /*TODO:propertyler buraya yazılacak PurchaseItemId = 1, PurchaseItemName = "deneme"*/});

            _purchaseItemRepository.Setup(x => x.Delete(It.IsAny<PurchaseItem>()));

            var handler = new DeletePurchaseItemCommandHandler(_purchaseItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _purchaseItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

