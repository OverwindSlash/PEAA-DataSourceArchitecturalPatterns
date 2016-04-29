using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderRelativeInfoDto
    {
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public short Quantity { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public float Discount { get; set; }
    }
}
