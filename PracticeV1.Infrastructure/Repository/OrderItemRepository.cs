using Microsoft.EntityFrameworkCore;
using PracticeV1.Application.DTO.Order;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.Repositories;
using PracticeV1.Domain.Entity;
using PracticeV1.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Infrastructure.Repository
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(PRDBContext context) : base(context) { }

        public async Task<List<OrderItem>> GetOrderItemsByUserID(int userId)
        {
          return _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .ThenInclude(o => o.User)
                .Where(oi => oi.Order.UserId == userId)
                 .OrderByDescending(oi => oi.Order.OrderDate)
                .ToList();


        }

        public async Task<PageResponse<OrderHistoryItem>> GetOrderItemsPageByUserID(int userId, PageRequest request)
        {
            var query = _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .Where(oi => oi.Order.UserId == userId);

            // TÌM KIẾM THEO TÊN SẢN PHẨM
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();
                query = query.Where(oi =>
                    oi.Product.Name.ToLower().Contains(term) ||
                    (oi.Product.Description != null && oi.Product.Description.ToLower().Contains(term))
                );
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(oi => oi.Order.OrderDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(oi => new OrderHistoryItem
                {
                    OrderId = oi.Order.Id,
                    OrderDate = oi.Order.OrderDate,
                    TotalAmount = oi.Order.TotalAmount,
                    Status = oi.Order.StatusOder.ToString(), 
                    ProductId = oi.Product.Id,
                    ProductName = oi.Product.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
            
                    ShippingAddress = oi.Order.ShippingAddress

                })
                .ToListAsync();

            return new PageResponse<OrderHistoryItem>
            {
                Items = items,
                TotalCount = totalItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

    }
}
