using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations.Schema;
using MySql.EntityFrameworkCore.Extensions;

namespace DataAccess.Concrete.Configurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("Id").HasColumnType("int")/*.ValueGeneratedOnAdd()*/;

            builder.Property(o => o.OrderDate).HasColumnType("datetime");

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId);

            builder.HasMany(o => o.OrderItems)
       .WithOne(oi => oi.Order)
       .HasForeignKey(oi => oi.OrderId);

            // Orders
            builder.HasData(
                new Order { Id = 1, OrderDate = DateTime.Now, CustomerId = 1 },
                new Order { Id = 2, OrderDate = DateTime.Now, CustomerId = 2 }
                // Diğer siparişler
            );
        }
    }
}
