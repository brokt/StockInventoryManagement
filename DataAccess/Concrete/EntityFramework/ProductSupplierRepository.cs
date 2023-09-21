
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ProductSupplierRepository : EfEntityRepositoryBase<ProductSupplier, ProjectDbContext>, IProductSupplierRepository
    {
        public ProductSupplierRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
