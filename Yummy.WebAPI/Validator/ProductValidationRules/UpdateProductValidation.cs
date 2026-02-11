using FluentValidation;
using Yummy.WebAPI.Dtos.ProductDto;

namespace Yummy.WebAPI.Validator.ProductValidationRules
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidation()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz")
                    .MaximumLength(35).WithMessage("Ürün adı maksimum 35 karakterden oluşmalıdır");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz")
                .MaximumLength(250).WithMessage("Açıklama en fazla 250 karakter olabilir");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Lütfen bir kategori seçin");
        }
    }
}
