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
    public class WarehouseEntityConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses");
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).HasColumnName("Id");
            builder.Property(w => w.Name).IsRequired().HasMaxLength(255);
            builder.Property(w => w.Location).HasMaxLength(500);

            builder.HasMany(w => w.StockMovements)
                   .WithOne(sm => sm.Warehouse)
                   .HasForeignKey(sm => sm.WarehouseId);

            // Warehouses
            builder.HasData(
                new Warehouse { Id = 1, Name = "Depo 1", Location = "Konum 1" },
                new Warehouse { Id = 2, Name = "Depo 2", Location = "Konum 2" }
                // Diğer depolar
            );
        }
    }
}
