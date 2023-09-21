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
    public class PurchaseItemConfiguration : IEntityTypeConfiguration<PurchaseItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseItem> builder)
        {
            builder.ToTable("PurchaseItems");
            builder.HasKey(pi => pi.Id);
            builder.Property(pi => pi.Id).HasColumnName("Id");
            builder.Property(pi => pi.Quantity).IsRequired();
            builder.Property(pi => pi.UnitPrice).HasColumnType("decimal(18, 2)");

            builder.HasOne(pi => pi.Product)
                   .WithMany()
                   .HasForeignKey(pi => pi.ProductId);

            // PurchaseItems
            builder.HasData(
                new PurchaseItem { Id = 1, Quantity = 5, UnitPrice = 9.99m, ProductId = 1, PurchaseId = 1 },
                new PurchaseItem { Id = 2, Quantity = 8, UnitPrice = 14.99m, ProductId = 2, PurchaseId = 2 }
                // Diğer alım kalemleri
            );
        }
    }
}
