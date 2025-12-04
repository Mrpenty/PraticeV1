using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.DTO.Category
{
    public class CategoryCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Createtime { get; set; } = DateTime.Now;
    }
}
