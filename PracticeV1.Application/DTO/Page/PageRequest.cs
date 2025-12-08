using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.DTO.Page
{
    public class PageRequest
    {
        public int  PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string SearchTerm { get; set; } = string.Empty;

        public string SortBy { get; set; } = "Id";
        public bool SortDescending { get; set; } = false;
    }
}
