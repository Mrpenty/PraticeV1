using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PracticeV1.Application.DTO.Category;
using PracticeV1.Application.DTO.Order;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.Services;

namespace PraticeV1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryCreate> _validator;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IValidator<CategoryCreate> validator)
        {
            _logger = logger;
            _categoryService = categoryService;
            _validator = validator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategoriesAsync([FromQuery] PageRequest request)
        {
            try
            {
                var categories = await _categoryService.GetCategoriesPageAsync(request);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all categories.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryCreate categoryName)
        {
            var validationResult = await _validator.ValidateAsync(categoryName);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryName);
                return Ok(createdCategory);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new category.");
                return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                var deleted = await _categoryService.DeleteCategoryAsync(id);
                if (!deleted)
                {
                    return NotFound("dw");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the category with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
