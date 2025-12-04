using FluentValidation;
using PracticeV1.Application.DTO.Order;
using PracticeV1.Application.Repository;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Validation
{
    public class CreateOrderValidator : AbstractValidator<CreateOrder>
    {

        private readonly IProductRepository _productRepository;
        public CreateOrderValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ID sản phẩm không hợp lệ");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0")
                .LessThanOrEqualTo(100).WithMessage("Tối đa 100 sản phẩm mỗi lần đặt");
            RuleFor(x =>x).MustAsync(async (order, cancellation) =>
                {
                    var product = await _productRepository.GetProductByIdAsync(order.ProductId);
                    if (product == null)
                    {
                        return false; 
                    }
                    return product.QuantityInStock >= order.Quantity;
                })
                .WithMessage( "Số lượng trong kho không đủ cho đơn hàng này.");

        }
       
    }


    public class OrderItemCreateValidator : AbstractValidator<OrderItemCreate>
    {
        public OrderItemCreateValidator()
        {
            RuleFor(item => item.ProductId)
                .GreaterThan(0).WithMessage("ID sản phẩm không hợp lệ");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0")
                .LessThanOrEqualTo(100).WithMessage("Không được đặt quá 100 sản phẩm/lần");
        }
    }

}

   
