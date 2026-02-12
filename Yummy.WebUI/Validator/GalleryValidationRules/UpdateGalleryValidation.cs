using FluentValidation;
using Yummy.WebUI.Dtos.GalleryDto;

namespace Yummy.WebUI.Validator.GalleryValidationRules
{
    public class UpdateGalleryValidation : AbstractValidator<UpdateGalleryDto>
    {
        public UpdateGalleryValidation()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel yolu boş geçilemez.");
        }
    }
}
