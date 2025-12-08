using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.DTO.Product;
using PracticeV1.Business.Service.Product;

namespace PraticeV1.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _productService;
        public readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllProductsAsync([FromQuery] PageRequest request)
        {
            try
            {
                var products = await _productService.GetProductsPageAsync(request);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all products.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpPut("Admin/{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] ProductCreate productCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(id, productCreate);
                if (updatedProduct == null)
                {
                    return NotFound();
                }
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the product with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpPost("Admin/Create")]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductCreate productCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdProduct = await _productService.CreateProductAsync(productCreate);
                return Ok(createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new product.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the product with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("Admin/Delete")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the product with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}