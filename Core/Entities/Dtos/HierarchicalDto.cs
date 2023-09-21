using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class HierarchicalDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }
    }
}
