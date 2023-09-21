using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using System.Reflection.Emit;

namespace DataAccess.Concrete.Configurations
{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.TransactionDate).HasColumnType("datetime");
            builder.Property(t => t.Amount).HasColumnType("decimal(18, 2)");

            builder.HasOne(t => t.CustomerAccount)
                   .WithMany(ca => ca.Transactions)
                   .HasForeignKey(t => t.CustomerAccountId);

            // Transactions
            builder.HasData(
                new Transaction { Id = 1, TransactionDate = DateTime.Now, Amount = 100, Type = TransactionType.Payment, CustomerAccountId = 1 },
                new Transaction { Id = 2, TransactionDate = DateTime.Now, Amount = 50, Type = TransactionType.Payment, CustomerAccountId = 2 }
                // Diğer işlemler
            );
        }
    }
}
