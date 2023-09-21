using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Configurations
{
    public class SupplierEntityConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("Id");
            builder.Property(s => s.Name).IsRequired().HasMaxLength(255);
            builder.Property(s => s.ContactInfo).HasMaxLength(500);
            builder.Property(s => s.Address).HasMaxLength(500);
            builder.Property(s => s.PaymentTerms).HasMaxLength(100);

            builder.HasMany(s => s.ProductSuppliers)
                   .WithOne(ps => ps.Supplier)
                   .HasForeignKey(ps => ps.SupplierId);

            builder.HasData(GetSuppliers());
        }

        public List<Supplier> GetSuppliers()
        {
            return new List<Supplier>()
            {
                 new Supplier { Id = 1, Name = "Tedarikçi 1", ContactInfo = "İletişim Bilgisi 1" },
                 new Supplier { Id = 2, Name = "Tedarikçi 2", ContactInfo = "İletişim Bilgisi 2" },
            };
        }
    }
}
