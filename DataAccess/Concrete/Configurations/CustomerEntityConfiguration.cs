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
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
            builder.Property(ca => ca.Email).HasMaxLength(30);
            builder.Property(ca => ca.PhoneNumber).HasMaxLength(30);
            builder.Property(ca => ca.Address).HasMaxLength(250);
            builder.Property(c => c.LoyaltyPoints).HasColumnType("decimal(18, 2)");

            builder.HasMany(c => c.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId);

            // Customers
            builder.HasData(
                new Customer { Id = 1, Name = "Müşteri 1", Address = "İletişim Bilgisi 1", Type = CustomerType.Individual, LoyaltyPoints = 100 },
                new Customer { Id = 2, Name = "Müşteri 2", Address = "İletişim Bilgisi 2", Type = CustomerType.Individual, LoyaltyPoints = 50 }
                // Diğer müşteriler
            );
        }
    }
}
