using PracticeV1.Application.DTO.Order;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Services
{
    public interface IOrderItemService
    {
        Task<List<OrderItem>> GetOrderItemsByUserID(int userId);
        Task<PageResponse<OrderHistoryItem>> GetOrderItemsPageByUserID(int userId, PageRequest request);
    }
}
