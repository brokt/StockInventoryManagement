
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class SaleRepository : EfEntityRepositoryBase<Sale, ProjectDbContext>, ISaleRepository
    {
        public SaleRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
