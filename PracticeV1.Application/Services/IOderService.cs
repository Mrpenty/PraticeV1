using PracticeV1.Application.DTO.Order;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Services
{
    public interface IOderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> CreateOrderAsync(int userId, CreateOrder createOrder);
        
    }
}
