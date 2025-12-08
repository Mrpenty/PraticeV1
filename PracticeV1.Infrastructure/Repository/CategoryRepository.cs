using Microsoft.EntityFrameworkCore;
using PracticeV1.Application.DTO.Category;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.Repositories;
using PracticeV1.Domain.Entity;
using PracticeV1.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Infrastructure.Repository
{
    public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PRDBContext context): base(context) { }


        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<PageResponse<Category>> GetPagedCategoriesAsync(PageRequest pageRequest, Expression<Func<Category, bool>>? fliter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null)
        {
           return await GetPageAsync(
                pageRequest: pageRequest,
                filter: fliter,
                orderBy: orderBy ?? (q => q.OrderByDescending(c => c.CreatedAt))
            );
        }
    }
}
