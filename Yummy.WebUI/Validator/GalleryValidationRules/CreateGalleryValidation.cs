using FluentValidation;
using Yummy.WebUI.Dtos.GalleryDto;

namespace Yummy.WebUI.Validator.GalleryValidationRules
{
    public class CreateGalleryValidation: AbstractValidator<CreateGalleryDto>
    {
        public CreateGalleryValidation()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel yolu boş geçilemez.");
        }
    }
}
