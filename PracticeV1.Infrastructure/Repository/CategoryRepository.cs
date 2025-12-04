using Microsoft.EntityFrameworkCore;
using PracticeV1.Application.DTO.Category;
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
    public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PRDBContext context): base(context) { }


        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
        }


    }
}
