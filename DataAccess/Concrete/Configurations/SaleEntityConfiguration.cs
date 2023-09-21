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
    public class SaleEntityConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("Id");
            builder.Property(s => s.SaleDate).HasColumnType("datetime");

            builder.HasOne(s => s.Customer)
                   .WithMany()
                   .HasForeignKey(s => s.CustomerId);

            builder.HasMany(s => s.SaleItems)
                   .WithOne(si => si.Sale)
                   .HasForeignKey(si => si.SaleId);

            // Sale
            builder.HasData(
                new Sale { Id = 1, SaleDate = DateTime.Now, CustomerId = 1 },
                new Sale { Id = 2, SaleDate = DateTime.Now, CustomerId = 2 }
                // Diğer satışlar
            );
        }
    }
}
