using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Business.Service.Product
{
    public interface IProductService
    {
        Task<List<Domain.Entity.Product>> GetAllProductsAsync();
        Task<Domain.Entity.Product?> GetProductByIdAsync(int id);
        Task<Domain.Entity.Product> CreateProductAsync(ProductCreate productCreate);
        Task<Domain.Entity.Product?> UpdateProductAsync(int id, ProductCreate productCreate);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> DecreaseStockAsync(int productId, int quantity);
        
        Task<PageResponse<Domain.Entity.Product?>> GetProductsPageAsync(PageRequest request);
    }
}
