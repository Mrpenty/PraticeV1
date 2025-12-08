using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PracticeV1.Application.DTO.Order;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.Repositories;
using PracticeV1.Application.Repository;
using PracticeV1.Application.Services;
using PracticeV1.Business.Service.Product;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Infrastructure.Service
{
    public class OderService : IOderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<User> _userManager;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<OderService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public OderService(IOrderRepository orderRepository, ILogger<OderService> logger, IUnitOfWork unitOfWork, IProductRepository productRepository, IEmailService emailService, UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _emailService = emailService;
            _userManager = userManager;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await Task.Run(() => _orderRepository.GetAllAsync());
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all orders.");
                throw;
            }
        }

        public async Task<Order> CreateOrderAsync(int userId, CreateOrder createOrder)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {

                    var products = await _productRepository.GetProductByIdAsync(createOrder.ProductId);
                    if (products == null)
                    {
                        throw new Exception("Product not found.");
                    }
                    if (products.QuantityInStock < createOrder.Quantity)
                    {
                        throw new Exception("Insufficient stock for the product.");
                    }
                    var success = await _productRepository.DecreaseStockAsync(createOrder.ProductId, createOrder.Quantity);
                    if (!success)
                    {
                        throw new Exception("Failed to decrease stock.");
                    }
                    var order = new Order
                    {
                        UserId = userId,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = products.Price * createOrder.Quantity,
                        StatusOder = StatusOder.Pending,
                        ShippingAddress = createOrder.ShippingAddress,
                    };
                    var orderItem = new OrderItem
                    {
                        ProductId = createOrder.ProductId,
                        Quantity = createOrder.Quantity,
                        UnitPrice = products.Price
                    };

                    order.OrderItems.Add(orderItem);

                
                 var createdOrders = await _orderRepository.CreateAsync(order);
                await _unitOfWork.SaveChangesAsync();
                var user = await _userManager.FindByIdAsync(userId.ToString()); 

                await _emailService.SendOrderConfirmationAsync(
                   orderId: createdOrders.Id,
                   customerName: user.FullName ?? "Khách hàng",
                   customerEmail: user.Email!,
                   totalAmount: createdOrders.TotalAmount 
                );
                await _unitOfWork.CommitAsync();
                return createdOrders;


            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "An error occurred while creating a new order.");
                throw;
            }


        }

       

        public Task<PageResponse<Order>> GetAllOrdersPageAsync(PageRequest request)
        {
            return _orderRepository.GetPagedOrdersAsync(request);
        }
    }
}