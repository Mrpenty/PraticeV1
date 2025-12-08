using FluentValidation;
using Microsoft.Identity.Client;
using PracticeV1.Application.DTO.Category;
using PracticeV1.Application.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Validation
{
    public class CategoryValidation : AbstractValidator<CategoryCreate>
    {
        public CategoryValidation(ICategoryRepository categoryRepository)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("yêu cầu tên danh mục .")
                .MaximumLength(100).WithMessage("Tên danh mục ko được quá 100 kí tự ");
            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Mô tả danh mục ko được quá 500 kí tự .");
            RuleFor(c => c.Name)
             .MustAsync(async (name, cancellation) =>
             {
                 var existing = await categoryRepository.GetCategoryByNameAsync(name);
                 return existing == null;
             })
             .WithMessage("Danh mục với tên này đã tồn tại!")
             .WithName("Name");
        }
    }
}
