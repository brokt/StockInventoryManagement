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
    public class SaleItemEntityConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");
            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id).HasColumnName("Id");
            builder.Property(si => si.Quantity).IsRequired();
            builder.Property(si => si.UnitPrice).HasColumnType("decimal(18, 2)");

            //builder.HasOne(si => si.Product)
            //       .WithMany()
            //       .HasForeignKey(si => si.ProductId);
            
            //builder.HasOne(si => si.Sale)
            //       .WithMany()
            //       .HasForeignKey(si => si.SaleId);

            // SaleItems
            builder.HasData(
                new SaleItem { Id = 1, Quantity = 3, UnitPrice = 12.99m, ProductId = 1, SaleId = 1 },
                new SaleItem { Id = 2, Quantity = 4, UnitPrice = 17.99m, ProductId = 2, SaleId = 2 }
                // Diğer satış kalemleri
            );
        }
    }
}
