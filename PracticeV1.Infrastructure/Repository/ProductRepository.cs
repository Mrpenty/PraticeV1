using Microsoft.EntityFrameworkCore;
using PracticeV1.Application.DTO.Product;
using PracticeV1.Infrastructure.Data;
using PracticeV1.Domain.Entity;
using PracticeV1.Application.Repository;


namespace PracticeV1.Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        public readonly PRDBContext _context;
        public ProductRepository(PRDBContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(ProductCreate productCreate)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Name == productCreate.Name);
            if (existingProduct != null)
            {
                throw new Exception("Product with the same name already exists.");
            }
            var product = new Product
            {
                Name = productCreate.Name,
                Description = productCreate.Description,
                Price = productCreate.Price,
            };


            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProductsByCriteriaAsync(string name)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<Product?> UpdateProductAsync(int id, ProductCreate productCreate)
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = productCreate.Name;
            existingProduct.Description = productCreate.Description;
            existingProduct.Price = productCreate.Price;
            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DecreaseStockAsync(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            // Assuming there is a Stock property in Product entity
            if (product.QuantityInStock < quantity)
            {
                return false; // Not enough stock
            }
            product.QuantityInStock -= quantity;
            return true;


        }
    }
}
