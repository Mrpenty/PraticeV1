using PracticeV1.Application.DTO.Category;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Services
{
    public interface ICategoryService 
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(CategoryCreate category);
        Task<bool> DeleteCategoryAsync(int id);

    }
}
