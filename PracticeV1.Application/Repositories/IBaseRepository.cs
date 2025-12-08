using PracticeV1.Application.DTO.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T?> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);

        Task<PageResponse<T>> GetPageAsync(PageRequest pageRequest, Expression<Func<T, bool>>? fliter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
       
        
    }
}
