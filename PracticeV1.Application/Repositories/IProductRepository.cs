using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.DTO.Product;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace PracticeV1.Application.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(ProductCreate productCreate);
        Task<Product?> UpdateProductAsync(int id, ProductCreate productCreate);
        Task<bool> DeleteProductAsync(int id);
        Task<List<Product>> GetProductsByCriteriaAsync(string name);
        Task<bool> DecreaseStockAsync(int productId, int quantity);

        Task<PageResponse<Product>> GetPagedProductsAsync(PageRequest request, Expression<Func<Product, bool>>? fliter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null);

    }
}
