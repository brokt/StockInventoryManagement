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
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Id).HasColumnName("Id");
            builder.Property(oi => oi.Quantity).IsRequired();
            builder.Property(oi => oi.UnitPrice).HasColumnType("decimal(18, 2)");

            builder.HasOne(oi => oi.Product)
                   .WithMany()
                   .HasForeignKey(oi => oi.ProductId);
            
            //builder.HasOne(oi => oi.Order)
            //       .WithMany()
            //       .HasForeignKey(oi => oi.OrderId);


            // OrderItems
            builder.HasData(
                new OrderItem { Id = 1, Quantity = 2, UnitPrice = 10.99m, ProductId = 1, OrderId = 1 },
                new OrderItem { Id = 2, Quantity = 3, UnitPrice = 15.99m, ProductId = 2, OrderId = 2 }
                // Diğer sipariş kalemleri
            );
        }
    }
}
