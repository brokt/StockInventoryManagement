using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class StockMovement : CrudBase, IEntity
    {
        public int Id { get; set; }
        public DateTime MovementDate { get; set; }
        public int Quantity { get; set; }
        public StockMovementType MovementType { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        // Diğer özellikler
    }

}
