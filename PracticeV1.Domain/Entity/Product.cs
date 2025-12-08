using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Domain.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }  

        public int QuantityInStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ProductStatus Status { get; set; } = ProductStatus.InStock;

    }

    public enum ProductStatus
    {
        InStock,
        OutOfStock,
        Discontinued
    }
}
