using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public abstract class CrudBase
    {
        //[Column(TypeName = "SmallDateTime")]
        public DateTime? CreatedAt { get; set; } = null;

        public int? CreatedById { get; set; }

        //[Column(TypeName = "SmallDateTime")]
        public DateTime? UpdatedAt { get; set; } = null;

        public int? UpdatedById { get; set; }

        //[Column(TypeName = "SmallDateTime")]
        public DateTime? DeletedAt { get; set; } = null;

        public long? DeletedById { get; set; }

        public bool IsDeleted { get; set; }
    }
}
