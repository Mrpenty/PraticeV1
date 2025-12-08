using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.Repositories;
using PracticeV1.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly PRDBContext _context;
        public BaseRepository(PRDBContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<PageResponse<T>> GetPageAsync(
     PageRequest pageRequest,
     Expression<Func<T, bool>>? filter = null,
     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = _context.Set<T>().AsQueryable();

            // 1. Filter
            if (filter != null)
                query = query.Where(filter);

            // 2. TÌM KIẾM TRÊN TẤT CẢ CÁC FIELD KIỂU STRING
            if (!string.IsNullOrWhiteSpace(pageRequest.SearchTerm))
            {
                var term = pageRequest.SearchTerm.Trim().ToLower();

                // Lấy tất cả property kiểu string
                var stringProperties = typeof(T)
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(string) && p.CanRead);

                if (stringProperties.Any())
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    Expression? combinedCondition = null;

                    foreach (var prop in stringProperties)
                    {
                        var propertyAccess = Expression.Property(parameter, prop);
                        var toLowerCall = Expression.Call(propertyAccess, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);
                        var containsCall = Expression.Call(
                            toLowerCall,
                            typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                            Expression.Constant(term));

                        combinedCondition = combinedCondition == null
                            ? containsCall
                            : Expression.OrElse(combinedCondition, containsCall);
                    }

                    if (combinedCondition != null)
                    {
                        var lambda = Expression.Lambda<Func<T, bool>>(combinedCondition, parameter);
                        query = query.Where(lambda);
                    }
                }
            }

            // 3. Đếm tổng
            var totalCount = await query.CountAsync();

            // 4. Sắp xếp
            if (orderBy != null)
                query = orderBy(query);
            else
                query = query.OrderByDescending(x => EF.Property<DateTime>(x, "CreatedAt"));

            // 5. Phân trang
            var items = await query
                .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
                .Take(pageRequest.PageSize)
                .ToListAsync();

            return new PageResponse<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize
            };
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


    }
}
