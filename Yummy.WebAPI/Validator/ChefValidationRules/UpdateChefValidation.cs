using FluentValidation;
using Yummy.WebAPI.Dtos.ChefDto;

namespace Yummy.WebAPI.Validator.ChefValidationRules
{
    public class UpdateChefValidation : AbstractValidator<UpdateChefDto>
    {
        public UpdateChefValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Şef adı boş bırakılamaz")
                .MaximumLength(50).WithMessage("Şef adı en fazla 50 karakter olmalıdır.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Ünvan alanı boş bırakılamaz")
                .MaximumLength(50).WithMessage("Ünvan en fazla 50 karakter olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz")
                .MaximumLength(250).WithMessage("Açıklama alanı çok uzun, lütfen kısaltın.");
        }
    }
}
