using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Transaction : CrudBase, IEntity
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public int CustomerAccountId { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        // Diğer özellikler
    }

}
