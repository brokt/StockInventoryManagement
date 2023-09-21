using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductSupplier : CrudBase, IEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }

}
