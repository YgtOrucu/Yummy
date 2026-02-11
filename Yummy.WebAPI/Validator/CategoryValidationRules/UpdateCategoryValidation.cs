using FluentValidation;
using Yummy.WebAPI.Dtos.CategoryDto;

namespace Yummy.WebAPI.Validator.CategoryValidationRules
{
    public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidation()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                   .MaximumLength(35).WithMessage("Kategori adı en fazla 30 karakter olmalıdır.")
                   .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.");
        }
    }
}
