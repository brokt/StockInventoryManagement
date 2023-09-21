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
    public class CustomerAccountEntityConfiguration : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.ToTable("CustomerAccounts");
            builder.HasKey(ca => ca.Id);
            builder.Property(ca => ca.Id).HasColumnName("Id");
            builder.Property(ca => ca.Balance).HasColumnType("decimal(18, 2)");


            builder.HasOne(ca => ca.Customer)
                   .WithOne(c => c.CustomerAccount)
                   .HasForeignKey<CustomerAccount>(ca => ca.CustomerId);

            builder.HasMany(ca => ca.Transactions)
                   .WithOne(t => t.CustomerAccount)
                   .HasForeignKey(t => t.CustomerAccountId);

            // CustomerAccounts
            builder.HasData(
                new CustomerAccount { Id = 1, CustomerId = 1, Balance = 500 },
                new CustomerAccount { Id = 2, CustomerId = 2, Balance = 250 }
                // Diğer müşteri hesapları
            );
        }
    }
}
