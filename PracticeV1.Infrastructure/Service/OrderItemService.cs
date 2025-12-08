using PracticeV1.Application.DTO.Order;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.Repositories;
using PracticeV1.Application.Services;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Infrastructure.Service
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<List<OrderItem>> GetOrderItemsByUserID(int userId)
        {
            var orderItems = await _orderItemRepository.GetOrderItemsByUserID(userId);

            return orderItems;
        }

       public Task<PageResponse<OrderHistoryItem>> GetOrderItemsPageByUserID(int userId, PageRequest request)
        {
            return _orderItemRepository.GetOrderItemsPageByUserID(userId, request);
        }
    }

}
