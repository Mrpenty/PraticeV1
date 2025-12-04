using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeV1.Application.DTO.Order;
using PracticeV1.Application.Services;

namespace PraticeV1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOderService _orderService;
        public readonly ILogger<OrderController> _logger;
        private readonly IValidator<CreateOrder> _validator;
        public OrderController(IOderService orderService, ILogger<OrderController> logger, IValidator<CreateOrder> validator)
        {
            _orderService = orderService;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all orders.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpPost("Order")]
        public async Task<IActionResult> CreateOrderAsync( [FromBody] CreateOrder createOrder)
        {
            var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

            var validationResult = await _validator.ValidateAsync(createOrder);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            try
            {
              
                var createdOrder = await _orderService.CreateOrderAsync(userId, createOrder);
                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new order.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
