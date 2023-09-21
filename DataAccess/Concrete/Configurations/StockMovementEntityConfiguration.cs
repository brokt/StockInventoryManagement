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
    public class StockMovementEntityConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements");
            builder.HasKey(sm => sm.Id);
            builder.Property(sm => sm.Id).HasColumnName("Id");
            builder.Property(sm => sm.MovementDate).HasColumnType("datetime");
            builder.Property(sm => sm.Quantity);
            builder.Property(sm => sm.MovementType).IsRequired();

            // İlişkileri tanımlamak için gerekli ayarlamaları yapabilirsiniz.
            builder.HasOne(sm => sm.Product)
                   .WithMany(p => p.StockMovements)
                   .HasForeignKey(sm => sm.ProductId);
            builder.HasOne(sm => sm.Warehouse)
                   .WithMany(w => w.StockMovements)
                   .HasForeignKey(sm => sm.WarehouseId);

            builder.HasData(GetStockMovements());
        }

        public List<StockMovement> GetStockMovements()
        {
            return new List<StockMovement>()
            {
                new StockMovement { Id = 1, MovementDate = DateTime.Now, Quantity = 10, MovementType = StockMovementType.StockIn, ProductId = 1, WarehouseId = 1 },
                new StockMovement { Id = 2, MovementDate = DateTime.Now, Quantity = -5, MovementType = StockMovementType.StockOut, ProductId = 2, WarehouseId = 1 },
            };
        }
    }
}
