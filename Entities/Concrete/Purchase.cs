using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Purchase : CrudBase, IEntity
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Supplier Supplier { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }
        // Diğer özellikler
    }

}
