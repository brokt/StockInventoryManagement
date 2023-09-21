using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Supplier : CrudBase, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string Address { get; set; } // Tedarikçi adresi
        public string PaymentTerms { get; set; } // Ödeme koşulları
        public List<ProductSupplier> ProductSuppliers { get; set; }
        // Diğer özellikler
    }

}
