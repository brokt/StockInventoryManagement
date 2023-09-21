
using Business.Handlers.SaleItems.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.SaleItems.Queries.GetSaleItemQuery;
using Entities.Concrete;
using static Business.Handlers.SaleItems.Queries.GetSaleItemsQuery;
using static Business.Handlers.SaleItems.Commands.CreateSaleItemCommand;
using Business.Handlers.SaleItems.Commands;
using Business.Constants;
using static Business.Handlers.SaleItems.Commands.UpdateSaleItemCommand;
using static Business.Handlers.SaleItems.Commands.DeleteSaleItemCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class SaleItemHandlerTests
    {
        Mock<ISaleItemRepository> _saleItemRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _saleItemRepository = new Mock<ISaleItemRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task SaleItem_GetQuery_Success()
        {
            //Arrange
            var query = new GetSaleItemQuery();

            _saleItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SaleItem, bool>>>())).ReturnsAsync(new SaleItem()
//propertyler buraya yazılacak
//{																		
//SaleItemId = 1,
//SaleItemName = "Test"
//}
);

            var handler = new GetSaleItemQueryHandler(_saleItemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.SaleItemId.Should().Be(1);

        }

        [Test]
        public async Task SaleItem_GetQueries_Success()
        {
            //Arrange
            var query = new GetSaleItemsQuery();

            _saleItemRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<SaleItem, bool>>>()))
                        .ReturnsAsync(new List<SaleItem> { new SaleItem() { /*TODO:propertyler buraya yazılacak SaleItemId = 1, SaleItemName = "test"*/ } });

            var handler = new GetSaleItemsQueryHandler(_saleItemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<SaleItem>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task SaleItem_CreateCommand_Success()
        {
            SaleItem rt = null;
            //Arrange
            var command = new CreateSaleItemCommand();
            //propertyler buraya yazılacak
            //command.SaleItemName = "deneme";

            _saleItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SaleItem, bool>>>()))
                        .ReturnsAsync(rt);

            _saleItemRepository.Setup(x => x.Add(It.IsAny<SaleItem>())).Returns(new SaleItem());

            var handler = new CreateSaleItemCommandHandler(_saleItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _saleItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task SaleItem_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateSaleItemCommand();
            //propertyler buraya yazılacak 
            //command.SaleItemName = "test";

            _saleItemRepository.Setup(x => x.Query())
                                           .Returns(new List<SaleItem> { new SaleItem() { /*TODO:propertyler buraya yazılacak SaleItemId = 1, SaleItemName = "test"*/ } }.AsQueryable());

            _saleItemRepository.Setup(x => x.Add(It.IsAny<SaleItem>())).Returns(new SaleItem());

            var handler = new CreateSaleItemCommandHandler(_saleItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task SaleItem_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateSaleItemCommand();
            //command.SaleItemName = "test";

            _saleItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SaleItem, bool>>>()))
                        .ReturnsAsync(new SaleItem() { /*TODO:propertyler buraya yazılacak SaleItemId = 1, SaleItemName = "deneme"*/ });

            _saleItemRepository.Setup(x => x.Update(It.IsAny<SaleItem>())).Returns(new SaleItem());

            var handler = new UpdateSaleItemCommandHandler(_saleItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _saleItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task SaleItem_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteSaleItemCommand();

            _saleItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<SaleItem, bool>>>()))
                        .ReturnsAsync(new SaleItem() { /*TODO:propertyler buraya yazılacak SaleItemId = 1, SaleItemName = "deneme"*/});

            _saleItemRepository.Setup(x => x.Delete(It.IsAny<SaleItem>()));

            var handler = new DeleteSaleItemCommandHandler(_saleItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _saleItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

