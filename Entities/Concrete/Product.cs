using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : CrudBase, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int CurrentStockQuantity { get; set; }
        public int MinimumStockQuantity { get; set; }
        public int MaximumStockQuantity { get; set; }
        public decimal Weight { get; set; } // Ürün ağırlığı
        public string Brand { get; set; } // Ürün markası
        public string ShelfNumber { get; set; } // Raf numarası
        public string BatchNumber { get; set; } // Parti numarası

        public List<StockMovement> StockMovements { get; set; }
        //public List<Category> Categories { get; set; }
        public List<ProductCategories> ProductCategories { get; set; } // Değişiklik burada
        public List<ProductSupplier> ProductSuppliers { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<SaleItem> SaleItems { get; set; }
        // Diğer özellikler ve metotlar
    }

}
