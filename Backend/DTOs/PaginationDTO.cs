using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; }
        public Filter Filter { get; set; }
        public string SearchText { get; set; }
    }

    public class Filter
    {
        public Categories[] Categories { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
    }

    public class Categories
    {
        public string Type { get; set; }
        public bool IsChecked { get; set; }
    }


}
