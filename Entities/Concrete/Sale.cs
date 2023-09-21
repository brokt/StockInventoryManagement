using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Sale : CrudBase, IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SaleItem> SaleItems { get; set; }
        // Diğer özellikler
    }

}
