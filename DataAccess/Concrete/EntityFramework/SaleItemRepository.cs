
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class SaleItemRepository : EfEntityRepositoryBase<SaleItem, ProjectDbContext>, ISaleItemRepository
    {
        public SaleItemRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
