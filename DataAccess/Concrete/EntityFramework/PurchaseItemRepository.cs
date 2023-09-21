
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class PurchaseItemRepository : EfEntityRepositoryBase<PurchaseItem, ProjectDbContext>, IPurchaseItemRepository
    {
        public PurchaseItemRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
