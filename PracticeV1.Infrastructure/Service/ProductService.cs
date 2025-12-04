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
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> CreateProductAsync(ProductCreate productCreate)
        {
            return await _productRepository.CreateProductAsync(productCreate);
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
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
            return await _productRepository.UpdateProductAsync(id, productCreate);
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
    }
}
