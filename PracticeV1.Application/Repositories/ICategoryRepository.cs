using PracticeV1.Application.DTO.Category;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category?> GetCategoryByNameAsync(string name);
        Task<PageResponse<Category>> GetPagedCategoriesAsync(PageRequest pageRequest, Expression<Func<Category, bool>>? fliter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null);
    }
}
