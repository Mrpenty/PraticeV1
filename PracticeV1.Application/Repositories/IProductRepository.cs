using PracticeV1.Application.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeV1.Domain.Entity;


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
      
    }
}
