
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OrderItemRepository : EfEntityRepositoryBase<OrderItem, ProjectDbContext>, IOrderItemRepository
    {
        public OrderItemRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
