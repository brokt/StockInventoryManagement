using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace DataAccess.Concrete.Configurations
{
    public class ProductSupplierEntityConfiguration : IEntityTypeConfiguration<ProductSupplier>
    {
        public void Configure(EntityTypeBuilder<ProductSupplier> builder)
        {
            builder.ToTable("ProductSuppliers"); // Tablo adı
            builder.HasKey(ps => new { ps.ProductId, ps.SupplierId }); // Birleşik anahtar

            builder.HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSuppliers)
                .HasForeignKey(ps => ps.ProductId);

            builder.HasOne(ps => ps.Supplier)
                .WithMany(s => s.ProductSuppliers)
                .HasForeignKey(ps => ps.SupplierId);

            // ProductSuppliers
            builder.HasData(
                new ProductSupplier { ProductId = 1, SupplierId = 1 },
                new ProductSupplier { ProductId = 2, SupplierId = 2 }
                // Diğer ürün-tedarikçi ilişkileri
            );
        }
    }
}
