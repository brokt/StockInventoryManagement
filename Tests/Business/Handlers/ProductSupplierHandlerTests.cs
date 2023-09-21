
using Business.Handlers.ProductSuppliers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProductSuppliers.Queries.GetProductSupplierQuery;
using Entities.Concrete;
using static Business.Handlers.ProductSuppliers.Queries.GetProductSuppliersQuery;
using static Business.Handlers.ProductSuppliers.Commands.CreateProductSupplierCommand;
using Business.Handlers.ProductSuppliers.Commands;
using Business.Constants;
using static Business.Handlers.ProductSuppliers.Commands.UpdateProductSupplierCommand;
using static Business.Handlers.ProductSuppliers.Commands.DeleteProductSupplierCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProductSupplierHandlerTests
    {
        Mock<IProductSupplierRepository> _productSupplierRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _productSupplierRepository = new Mock<IProductSupplierRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProductSupplier_GetQuery_Success()
        {
            //Arrange
            var query = new GetProductSupplierQuery();

            _productSupplierRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductSupplier, bool>>>())).ReturnsAsync(new ProductSupplier()
//propertyler buraya yazılacak
//{																		
//ProductSupplierId = 1,
//ProductSupplierName = "Test"
//}
);

            var handler = new GetProductSupplierQueryHandler(_productSupplierRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProductSupplierId.Should().Be(1);

        }

        [Test]
        public async Task ProductSupplier_GetQueries_Success()
        {
            //Arrange
            var query = new GetProductSuppliersQuery();

            _productSupplierRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProductSupplier, bool>>>()))
                        .ReturnsAsync(new List<ProductSupplier> { new ProductSupplier() { /*TODO:propertyler buraya yazılacak ProductSupplierId = 1, ProductSupplierName = "test"*/ } });

            var handler = new GetProductSuppliersQueryHandler(_productSupplierRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProductSupplier>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProductSupplier_CreateCommand_Success()
        {
            ProductSupplier rt = null;
            //Arrange
            var command = new CreateProductSupplierCommand();
            //propertyler buraya yazılacak
            //command.ProductSupplierName = "deneme";

            _productSupplierRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductSupplier, bool>>>()))
                        .ReturnsAsync(rt);

            _productSupplierRepository.Setup(x => x.Add(It.IsAny<ProductSupplier>())).Returns(new ProductSupplier());

            var handler = new CreateProductSupplierCommandHandler(_productSupplierRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _productSupplierRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProductSupplier_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProductSupplierCommand();
            //propertyler buraya yazılacak 
            //command.ProductSupplierName = "test";

            _productSupplierRepository.Setup(x => x.Query())
                                           .Returns(new List<ProductSupplier> { new ProductSupplier() { /*TODO:propertyler buraya yazılacak ProductSupplierId = 1, ProductSupplierName = "test"*/ } }.AsQueryable());

            _productSupplierRepository.Setup(x => x.Add(It.IsAny<ProductSupplier>())).Returns(new ProductSupplier());

            var handler = new CreateProductSupplierCommandHandler(_productSupplierRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProductSupplier_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProductSupplierCommand();
            //command.ProductSupplierName = "test";

            _productSupplierRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductSupplier, bool>>>()))
                        .ReturnsAsync(new ProductSupplier() { /*TODO:propertyler buraya yazılacak ProductSupplierId = 1, ProductSupplierName = "deneme"*/ });

            _productSupplierRepository.Setup(x => x.Update(It.IsAny<ProductSupplier>())).Returns(new ProductSupplier());

            var handler = new UpdateProductSupplierCommandHandler(_productSupplierRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _productSupplierRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProductSupplier_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProductSupplierCommand();

            _productSupplierRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductSupplier, bool>>>()))
                        .ReturnsAsync(new ProductSupplier() { /*TODO:propertyler buraya yazılacak ProductSupplierId = 1, ProductSupplierName = "deneme"*/});

            _productSupplierRepository.Setup(x => x.Delete(It.IsAny<ProductSupplier>()));

            var handler = new DeleteProductSupplierCommandHandler(_productSupplierRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _productSupplierRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

