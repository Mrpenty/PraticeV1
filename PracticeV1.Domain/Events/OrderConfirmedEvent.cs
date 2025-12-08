using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Domain.Events
{
    public class OrderConfirmedEvent
    {
        

        public int OrderId { get; }
        public string CustomerEmail { get; }
        public string CustomerName { get; }
        public decimal TotalAmount { get; }
        public DateTime OrderDate { get; }

        public OrderConfirmedEvent(int orderId, string customerEmail, string customerName, decimal totalAmount, DateTime orderDate)
        {
            OrderId = orderId;
            CustomerEmail = customerEmail;
            CustomerName = customerName;
            TotalAmount = totalAmount;
            OrderDate = orderDate;
        }
    }
}
