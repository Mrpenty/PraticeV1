using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PracticeV1.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        
        public decimal TotalAmount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusOder StatusOder { get; set; } = StatusOder.Pending;
        public string? ShippingAddress { get; set; } = null!;

        public int UserId { get; set; }
        public User? User { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public enum StatusOder
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Canceled
    }
}
