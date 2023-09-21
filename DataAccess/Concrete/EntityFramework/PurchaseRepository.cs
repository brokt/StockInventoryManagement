
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class PurchaseRepository : EfEntityRepositoryBase<Purchase, ProjectDbContext>, IPurchaseRepository
    {
        public PurchaseRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
