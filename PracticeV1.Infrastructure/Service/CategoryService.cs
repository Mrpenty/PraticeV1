using FluentValidation;
using FluentValidation.Results;
using PracticeV1.Application.DTO.Category;
using PracticeV1.Application.Repositories;
using PracticeV1.Application.Services;
using PracticeV1.Domain.Entity;
using PracticeV1.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Infrastructure.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;   
        private readonly IValidator<CategoryCreate> _validator; 
        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IValidator<CategoryCreate> validator)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;

        }

        public async Task<Category> CreateCategoryAsync(CategoryCreate categoryc)
        {

            //ValidationResult validationResult = await _validator.ValidateAsync(categoryc);
            //if (!validationResult.IsValid)
            //{
            //    throw new ValidationException(validationResult.Errors);
            //}

            var existing = await _categoryRepository.GetCategoryByNameAsync(categoryc.Name);
            if (existing != null)
            {
                throw new InvalidOperationException("Danh mục với tên này đã tồn tại!");
            }

            var category = new Category
            {
                Name = categoryc.Name,
                Description = categoryc.Description,
                CreatedAt = DateTime.UtcNow,
               
            };

            try
            {
                var createdCategory = await _categoryRepository.CreateAsync(category);
                await _unitOfWork.SaveChangesAsync();

                return createdCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }
           
        

        public Task<bool> DeleteCategoryAsync(int id)
        {
           var result =  Task.Run(() => _categoryRepository.DeleteAsync(id));
            return result;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await Task.Run(() => _categoryRepository.GetAllAsync());
                return categories;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
