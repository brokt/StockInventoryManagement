
using Business.Handlers.CustomerAccounts.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.CustomerAccounts.Queries.GetCustomerAccountQuery;
using Entities.Concrete;
using static Business.Handlers.CustomerAccounts.Queries.GetCustomerAccountsQuery;
using static Business.Handlers.CustomerAccounts.Commands.CreateCustomerAccountCommand;
using Business.Handlers.CustomerAccounts.Commands;
using Business.Constants;
using static Business.Handlers.CustomerAccounts.Commands.UpdateCustomerAccountCommand;
using static Business.Handlers.CustomerAccounts.Commands.DeleteCustomerAccountCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CustomerAccountHandlerTests
    {
        Mock<ICustomerAccountRepository> _customerAccountRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _customerAccountRepository = new Mock<ICustomerAccountRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task CustomerAccount_GetQuery_Success()
        {
            //Arrange
            var query = new GetCustomerAccountQuery();

            _customerAccountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerAccount, bool>>>())).ReturnsAsync(new CustomerAccount()
//propertyler buraya yazılacak
//{																		
//CustomerAccountId = 1,
//CustomerAccountName = "Test"
//}
);

            var handler = new GetCustomerAccountQueryHandler(_customerAccountRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CustomerAccountId.Should().Be(1);

        }

        [Test]
        public async Task CustomerAccount_GetQueries_Success()
        {
            //Arrange
            var query = new GetCustomerAccountsQuery();

            _customerAccountRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<CustomerAccount, bool>>>()))
                        .ReturnsAsync(new List<CustomerAccount> { new CustomerAccount() { /*TODO:propertyler buraya yazılacak CustomerAccountId = 1, CustomerAccountName = "test"*/ } });

            var handler = new GetCustomerAccountsQueryHandler(_customerAccountRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<CustomerAccount>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task CustomerAccount_CreateCommand_Success()
        {
            CustomerAccount rt = null;
            //Arrange
            var command = new CreateCustomerAccountCommand();
            //propertyler buraya yazılacak
            //command.CustomerAccountName = "deneme";

            _customerAccountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerAccount, bool>>>()))
                        .ReturnsAsync(rt);

            _customerAccountRepository.Setup(x => x.Add(It.IsAny<CustomerAccount>())).Returns(new CustomerAccount());

            var handler = new CreateCustomerAccountCommandHandler(_customerAccountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerAccountRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task CustomerAccount_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCustomerAccountCommand();
            //propertyler buraya yazılacak 
            //command.CustomerAccountName = "test";

            _customerAccountRepository.Setup(x => x.Query())
                                           .Returns(new List<CustomerAccount> { new CustomerAccount() { /*TODO:propertyler buraya yazılacak CustomerAccountId = 1, CustomerAccountName = "test"*/ } }.AsQueryable());

            _customerAccountRepository.Setup(x => x.Add(It.IsAny<CustomerAccount>())).Returns(new CustomerAccount());

            var handler = new CreateCustomerAccountCommandHandler(_customerAccountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task CustomerAccount_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCustomerAccountCommand();
            //command.CustomerAccountName = "test";

            _customerAccountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerAccount, bool>>>()))
                        .ReturnsAsync(new CustomerAccount() { /*TODO:propertyler buraya yazılacak CustomerAccountId = 1, CustomerAccountName = "deneme"*/ });

            _customerAccountRepository.Setup(x => x.Update(It.IsAny<CustomerAccount>())).Returns(new CustomerAccount());

            var handler = new UpdateCustomerAccountCommandHandler(_customerAccountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerAccountRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task CustomerAccount_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCustomerAccountCommand();

            _customerAccountRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CustomerAccount, bool>>>()))
                        .ReturnsAsync(new CustomerAccount() { /*TODO:propertyler buraya yazılacak CustomerAccountId = 1, CustomerAccountName = "deneme"*/});

            _customerAccountRepository.Setup(x => x.Delete(It.IsAny<CustomerAccount>()));

            var handler = new DeleteCustomerAccountCommandHandler(_customerAccountRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerAccountRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

