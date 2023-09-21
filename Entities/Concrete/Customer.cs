using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer : CrudBase, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public CustomerType Type { get; set; } // Müşteri türü
        public GenderType GenderType { get; set; } // Müşteri Cinsiyet türü
        public decimal LoyaltyPoints { get; set; } // Müşteri sadakat puanları
        public List<Order> Orders { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        // Diğer özellikler
    }

}
