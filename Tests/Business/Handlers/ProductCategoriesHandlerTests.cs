
using Business.Handlers.ProductCategorieses.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProductCategorieses.Queries.GetProductCategoriesQuery;
using Entities.Concrete;
using static Business.Handlers.ProductCategorieses.Queries.GetProductCategoriesesQuery;
using static Business.Handlers.ProductCategorieses.Commands.CreateProductCategoriesCommand;
using Business.Handlers.ProductCategorieses.Commands;
using Business.Constants;
using static Business.Handlers.ProductCategorieses.Commands.UpdateProductCategoriesCommand;
using static Business.Handlers.ProductCategorieses.Commands.DeleteProductCategoriesCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProductCategoriesHandlerTests
    {
        Mock<IProductCategoriesRepository> _productCategoriesRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _productCategoriesRepository = new Mock<IProductCategoriesRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProductCategories_GetQuery_Success()
        {
            //Arrange
            var query = new GetProductCategoriesQuery();

            _productCategoriesRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductCategories, bool>>>())).ReturnsAsync(new ProductCategories()
//propertyler buraya yazılacak
//{																		
//ProductCategoriesId = 1,
//ProductCategoriesName = "Test"
//}
);

            var handler = new GetProductCategoriesQueryHandler(_productCategoriesRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProductCategoriesId.Should().Be(1);

        }

        [Test]
        public async Task ProductCategories_GetQueries_Success()
        {
            //Arrange
            var query = new GetProductCategoriesesQuery();

            _productCategoriesRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProductCategories, bool>>>()))
                        .ReturnsAsync(new List<ProductCategories> { new ProductCategories() { /*TODO:propertyler buraya yazılacak ProductCategoriesId = 1, ProductCategoriesName = "test"*/ } });

            var handler = new GetProductCategoriesesQueryHandler(_productCategoriesRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProductCategories>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProductCategories_CreateCommand_Success()
        {
            ProductCategories rt = null;
            //Arrange
            var command = new CreateProductCategoriesCommand();
            //propertyler buraya yazılacak
            //command.ProductCategoriesName = "deneme";

            _productCategoriesRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductCategories, bool>>>()))
                        .ReturnsAsync(rt);

            _productCategoriesRepository.Setup(x => x.Add(It.IsAny<ProductCategories>())).Returns(new ProductCategories());

            var handler = new CreateProductCategoriesCommandHandler(_productCategoriesRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _productCategoriesRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProductCategories_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProductCategoriesCommand();
            //propertyler buraya yazılacak 
            //command.ProductCategoriesName = "test";

            _productCategoriesRepository.Setup(x => x.Query())
                                           .Returns(new List<ProductCategories> { new ProductCategories() { /*TODO:propertyler buraya yazılacak ProductCategoriesId = 1, ProductCategoriesName = "test"*/ } }.AsQueryable());

            _productCategoriesRepository.Setup(x => x.Add(It.IsAny<ProductCategories>())).Returns(new ProductCategories());

            var handler = new CreateProductCategoriesCommandHandler(_productCategoriesRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProductCategories_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProductCategoriesCommand();
            //command.ProductCategoriesName = "test";

            _productCategoriesRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductCategories, bool>>>()))
                        .ReturnsAsync(new ProductCategories() { /*TODO:propertyler buraya yazılacak ProductCategoriesId = 1, ProductCategoriesName = "deneme"*/ });

            _productCategoriesRepository.Setup(x => x.Update(It.IsAny<ProductCategories>())).Returns(new ProductCategories());

            var handler = new UpdateProductCategoriesCommandHandler(_productCategoriesRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _productCategoriesRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProductCategories_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProductCategoriesCommand();

            _productCategoriesRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductCategories, bool>>>()))
                        .ReturnsAsync(new ProductCategories() { /*TODO:propertyler buraya yazılacak ProductCategoriesId = 1, ProductCategoriesName = "deneme"*/});

            _productCategoriesRepository.Setup(x => x.Delete(It.IsAny<ProductCategories>()));

            var handler = new DeleteProductCategoriesCommandHandler(_productCategoriesRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _productCategoriesRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

