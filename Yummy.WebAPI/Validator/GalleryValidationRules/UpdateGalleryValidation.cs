using FluentValidation;
using Yummy.WebAPI.Dtos.GalleryDto;

namespace Yummy.WebAPI.Validator.GalleryValidationRules
{
    public class UpdateGalleryValidation : AbstractValidator<UpdateGalleryDto>
    {
        public UpdateGalleryValidation()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel yolu boş geçilemez.");
        }
    }
}
