using FluentValidation;
using Yummy.WebUI.Dtos.ChefDto;

namespace Yummy.WebUI.Validator.ChefValidationRules
{
    public class CreateChefValidation : AbstractValidator<CreateChefDto>
    {
        public CreateChefValidation()
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
