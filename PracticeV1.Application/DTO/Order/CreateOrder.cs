using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.DTO.Order
{
    public class CreateOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ShippingAddress { get; set; }

        public string OrderStatus { get; set; } 
    }
}
