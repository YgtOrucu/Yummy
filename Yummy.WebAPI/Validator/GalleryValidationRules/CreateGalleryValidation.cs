using FluentValidation;
using Yummy.WebAPI.Dtos.GalleryDto;

namespace Yummy.WebAPI.Validator.GalleryValidationRules
{
    public class CreateGalleryValidation: AbstractValidator<CreateGalleryDto>
    {
        public CreateGalleryValidation()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel yolu boş geçilemez.");
        }
    }
}
