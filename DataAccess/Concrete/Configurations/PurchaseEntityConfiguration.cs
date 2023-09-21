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
    public class PurchaseEntityConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchases");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.PurchaseDate).HasColumnType("datetime");

            builder.HasOne(p => p.Supplier)
                   .WithMany()
                   .HasForeignKey(p => p.SupplierId);

            builder.HasMany(p => p.PurchaseItems)
                   .WithOne(pi => pi.Purchase)
                   .HasForeignKey(pi => pi.PurchaseId);

            // Purchases
            builder.HasData(
                new Purchase { Id = 1, PurchaseDate = DateTime.Now, SupplierId = 1 },
                new Purchase { Id = 2, PurchaseDate = DateTime.Now, SupplierId = 2 }
                // Diğer alımlar
            );
        }
    }
}
