using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category : CrudBase, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; } // Üst kategori kimliği
        public Category ParentCategory { get; set; } // Üst kategori ilişkisi
        //public List<Product> Products { get; set; }
        public List<ProductCategories> ProductCategories { get; set; }
        // Diğer özellikler
    }

}
