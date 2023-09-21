
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class StockMovementRepository : EfEntityRepositoryBase<StockMovement, ProjectDbContext>, IStockMovementRepository
    {
        public StockMovementRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
