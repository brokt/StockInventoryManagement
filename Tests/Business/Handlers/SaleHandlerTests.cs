
using Business.Handlers.Sales.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Sales.Queries.GetSaleQuery;
using Entities.Concrete;
using static Business.Handlers.Sales.Queries.GetSalesQuery;
using static Business.Handlers.Sales.Commands.CreateSaleCommand;
using Business.Handlers.Sales.Commands;
using Business.Constants;
using static Business.Handlers.Sales.Commands.UpdateSaleCommand;
using static Business.Handlers.Sales.Commands.DeleteSaleCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class SaleHandlerTests
    {
        Mock<ISaleRepository> _saleRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _saleRepository = new Mock<ISaleRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Sale_GetQuery_Success()
        {
            //Arrange
            var query = new GetSaleQuery();

            _saleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Sale, bool>>>())).ReturnsAsync(new Sale()
//propertyler buraya yazılacak
//{																		
//SaleId = 1,
//SaleName = "Test"
//}
);

            var handler = new GetSaleQueryHandler(_saleRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.SaleId.Should().Be(1);

        }

        [Test]
        public async Task Sale_GetQueries_Success()
        {
            //Arrange
            var query = new GetSalesQuery();

            _saleRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Sale, bool>>>()))
                        .ReturnsAsync(new List<Sale> { new Sale() { /*TODO:propertyler buraya yazılacak SaleId = 1, SaleName = "test"*/ } });

            var handler = new GetSalesQueryHandler(_saleRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Sale>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Sale_CreateCommand_Success()
        {
            Sale rt = null;
            //Arrange
            var command = new CreateSaleCommand();
            //propertyler buraya yazılacak
            //command.SaleName = "deneme";

            _saleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Sale, bool>>>()))
                        .ReturnsAsync(rt);

            _saleRepository.Setup(x => x.Add(It.IsAny<Sale>())).Returns(new Sale());

            var handler = new CreateSaleCommandHandler(_saleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _saleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Sale_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateSaleCommand();
            //propertyler buraya yazılacak 
            //command.SaleName = "test";

            _saleRepository.Setup(x => x.Query())
                                           .Returns(new List<Sale> { new Sale() { /*TODO:propertyler buraya yazılacak SaleId = 1, SaleName = "test"*/ } }.AsQueryable());

            _saleRepository.Setup(x => x.Add(It.IsAny<Sale>())).Returns(new Sale());

            var handler = new CreateSaleCommandHandler(_saleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Sale_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateSaleCommand();
            //command.SaleName = "test";

            _saleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Sale, bool>>>()))
                        .ReturnsAsync(new Sale() { /*TODO:propertyler buraya yazılacak SaleId = 1, SaleName = "deneme"*/ });

            _saleRepository.Setup(x => x.Update(It.IsAny<Sale>())).Returns(new Sale());

            var handler = new UpdateSaleCommandHandler(_saleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _saleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Sale_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteSaleCommand();

            _saleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Sale, bool>>>()))
                        .ReturnsAsync(new Sale() { /*TODO:propertyler buraya yazılacak SaleId = 1, SaleName = "deneme"*/});

            _saleRepository.Setup(x => x.Delete(It.IsAny<Sale>()));

            var handler = new DeleteSaleCommandHandler(_saleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _saleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

