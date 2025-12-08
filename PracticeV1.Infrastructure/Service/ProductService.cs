using Microsoft.Extensions.Caching.Memory;
using PracticeV1.Application.DTO.Page;
using PracticeV1.Application.DTO.Product;
using PracticeV1.Application.Repository;
using PracticeV1.Business.Service.Product;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Service
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _productRepository;
        public readonly IMemoryCache _memoryCache;
        private const string ProductCacheKey = "Product";
        private const string ProductPageCacheKey = "ProductPage";


        public ProductService(IProductRepository productRepository, IMemoryCache memoryCache)
        {
            _productRepository = productRepository;
            _memoryCache = memoryCache; 
        }
        public async Task<Product> CreateProductAsync(ProductCreate productCreate)
        {
            var product = await _productRepository.CreateProductAsync(productCreate);

            _memoryCache.Remove(ProductCacheKey);
            return product; 
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var result = await _productRepository.DeleteProductAsync(id);
            if (result)
            {
                _memoryCache.Remove(ProductCacheKey);
            }
            return result;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
        public async Task<List<Product>> GetProductsByCriteriaAsync(string name)
        {
            return await _productRepository.GetProductsByCriteriaAsync(name);
        }
        public async Task<Product?> UpdateProductAsync(int id, ProductCreate productCreate)
        {
            var product = await _productRepository.UpdateProductAsync(id, productCreate);
            if (product != null)
            {
                _memoryCache.Remove(ProductCacheKey);
            }
            return product;
        }
        

        public async Task<bool> DecreaseStockAsync(int productId, int quantity)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null || product.QuantityInStock < quantity)
            {
                return false; 
            }
            product.QuantityInStock -= quantity;
            await _productRepository.UpdateProductAsync(productId, new ProductCreate
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock
            });
            return true;
        }

        public async Task<PageResponse<Product?>> GetProductsPageAsync(PageRequest request)
        {
            var cateKey = string.Format(ProductPageCacheKey, request.PageNumber, request.PageSize, request.SearchTerm ?? null);
            if(_memoryCache.TryGetValue(cateKey, out PageResponse<Product?> cachedProducts))
            {
                return cachedProducts;
            }
            var productsPage = await _productRepository.GetPagedProductsAsync(request);
            _memoryCache.Set(cateKey, productsPage, TimeSpan.FromSeconds(10));
            return productsPage;
        }
    }
}
