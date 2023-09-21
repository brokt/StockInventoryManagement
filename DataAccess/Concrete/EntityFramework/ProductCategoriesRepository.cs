
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ProductCategoriesRepository : EfEntityRepositoryBase<ProductCategories, ProjectDbContext>, IProductCategoriesRepository
    {
        public ProductCategoriesRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
