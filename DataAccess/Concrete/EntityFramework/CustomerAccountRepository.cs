
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class CustomerAccountRepository : EfEntityRepositoryBase<CustomerAccount, ProjectDbContext>, ICustomerAccountRepository
    {
        public CustomerAccountRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
