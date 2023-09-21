using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.SKU).HasMaxLength(50);
            //builder.Property(p => p.UnitPrice).HasColumnType("decimal(18, 2)");
            //builder.Property(p => p.CostPrice).HasColumnType("decimal(18, 2)");
            //builder.Property(p => p.Weight).HasColumnType("decimal(18, 2)");
            builder.Property(p => p.Brand).HasMaxLength(100);
            builder.Property(p => p.ShelfNumber).HasMaxLength(50);
            builder.Property(p => p.BatchNumber).HasMaxLength(50);

            // İlişkileri tanımlamak için gerekli ayarlamaları yapabilirsiniz.
            // StockMovements ilişkisi
            builder.HasMany(p => p.StockMovements)
                   .WithOne(sm => sm.Product)
                   .HasForeignKey(sm => sm.ProductId);

            // ProductCategories ilişkisi
            builder.HasMany(p => p.ProductCategories)
                   .WithOne(ps => ps.Product)
                   .HasForeignKey(ps => ps.ProductId);
            
            // ProductSuppliers ilişkisi
            builder.HasMany(p => p.ProductSuppliers)
                   .WithOne(ps => ps.Product)
                   .HasForeignKey(ps => ps.ProductId);

            // SaleItems ilişkisi
            builder.HasMany(p => p.SaleItems)
                   .WithOne(ps => ps.Product)
                   .HasForeignKey(ps => ps.ProductId)
                   .HasPrincipalKey(e => e.Id);
            
            // OrderItems ilişkisi
            builder.HasMany(p => p.OrderItems)
                   .WithOne(ps => ps.Product)
                   .HasForeignKey(ps => ps.ProductId)
                   .HasPrincipalKey(e => e.Id);

            builder.HasData(GetCategories());
        }

        private List<Product> GetCategories()
        {
            return new List<Product>()
            {
                new Product { Id = 1, Name = "Ürün 1", Description = "Açıklama 1", SKU = "SKU1", UnitPrice = 10.99m },
                new Product { Id = 2, Name = "Ürün 2", Description = "Açıklama 2", SKU = "SKU2", UnitPrice = 15.99m },
            };
        }
    }
}
