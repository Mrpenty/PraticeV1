using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PracticeV1.Application.DTO.Order
{
    public class OrderHistoryItem
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;     
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal => UnitPrice * Quantity;
        public string ShippingAddress { get; set; } = null!;
    }
}
