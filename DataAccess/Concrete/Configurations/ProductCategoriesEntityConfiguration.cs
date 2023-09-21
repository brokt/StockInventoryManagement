using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Concrete.Configurations
{
    public class ProductCategoriesEntityConfiguration : IEntityTypeConfiguration<ProductCategories>
    {
        public void Configure(EntityTypeBuilder<ProductCategories> builder)
        {
            builder.HasKey(cp => new { cp.CategoryId, cp.ProductId });

            builder.HasOne(cp => cp.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(cp => cp.CategoryId);

            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(cp => cp.ProductId);

            builder.HasData(
        new { CategoryId = 1, ProductId = 1 },
        new { CategoryId = 2, ProductId = 1 },
        new { CategoryId = 2, ProductId = 2 }
        // Diğer ilişkiler
    );
        }
    }
}
