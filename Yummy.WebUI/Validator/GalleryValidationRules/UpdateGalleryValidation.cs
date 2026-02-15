using FluentValidation;
using Yummy.WebUI.Dtos.GalleryDto;

namespace Yummy.WebUI.Validator.GalleryValidationRules
{
    public class UpdateGalleryValidation : AbstractValidator<UpdateGalleryDto>
    {
        public UpdateGalleryValidation()
        {
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Görsel yolu boş geçilemez.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş geçilemez.");
        }
    }
}
