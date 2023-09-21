
using Business.Handlers.OrderItems.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OrderItems.Queries.GetOrderItemQuery;
using Entities.Concrete;
using static Business.Handlers.OrderItems.Queries.GetOrderItemsQuery;
using static Business.Handlers.OrderItems.Commands.CreateOrderItemCommand;
using Business.Handlers.OrderItems.Commands;
using Business.Constants;
using static Business.Handlers.OrderItems.Commands.UpdateOrderItemCommand;
using static Business.Handlers.OrderItems.Commands.DeleteOrderItemCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrderItemHandlerTests
    {
        Mock<IOrderItemRepository> _orderItemRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _orderItemRepository = new Mock<IOrderItemRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OrderItem_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrderItemQuery();

            _orderItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderItem, bool>>>())).ReturnsAsync(new OrderItem()
//propertyler buraya yazılacak
//{																		
//OrderItemId = 1,
//OrderItemName = "Test"
//}
);

            var handler = new GetOrderItemQueryHandler(_orderItemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrderItemId.Should().Be(1);

        }

        [Test]
        public async Task OrderItem_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrderItemsQuery();

            _orderItemRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OrderItem, bool>>>()))
                        .ReturnsAsync(new List<OrderItem> { new OrderItem() { /*TODO:propertyler buraya yazılacak OrderItemId = 1, OrderItemName = "test"*/ } });

            var handler = new GetOrderItemsQueryHandler(_orderItemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OrderItem>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OrderItem_CreateCommand_Success()
        {
            OrderItem rt = null;
            //Arrange
            var command = new CreateOrderItemCommand();
            //propertyler buraya yazılacak
            //command.OrderItemName = "deneme";

            _orderItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderItem, bool>>>()))
                        .ReturnsAsync(rt);

            _orderItemRepository.Setup(x => x.Add(It.IsAny<OrderItem>())).Returns(new OrderItem());

            var handler = new CreateOrderItemCommandHandler(_orderItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orderItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OrderItem_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrderItemCommand();
            //propertyler buraya yazılacak 
            //command.OrderItemName = "test";

            _orderItemRepository.Setup(x => x.Query())
                                           .Returns(new List<OrderItem> { new OrderItem() { /*TODO:propertyler buraya yazılacak OrderItemId = 1, OrderItemName = "test"*/ } }.AsQueryable());

            _orderItemRepository.Setup(x => x.Add(It.IsAny<OrderItem>())).Returns(new OrderItem());

            var handler = new CreateOrderItemCommandHandler(_orderItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OrderItem_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrderItemCommand();
            //command.OrderItemName = "test";

            _orderItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderItem, bool>>>()))
                        .ReturnsAsync(new OrderItem() { /*TODO:propertyler buraya yazılacak OrderItemId = 1, OrderItemName = "deneme"*/ });

            _orderItemRepository.Setup(x => x.Update(It.IsAny<OrderItem>())).Returns(new OrderItem());

            var handler = new UpdateOrderItemCommandHandler(_orderItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orderItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OrderItem_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrderItemCommand();

            _orderItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OrderItem, bool>>>()))
                        .ReturnsAsync(new OrderItem() { /*TODO:propertyler buraya yazılacak OrderItemId = 1, OrderItemName = "deneme"*/});

            _orderItemRepository.Setup(x => x.Delete(It.IsAny<OrderItem>()));

            var handler = new DeleteOrderItemCommandHandler(_orderItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _orderItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

