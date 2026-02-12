using FluentValidation;
using Yummy.WebUI.Dtos.HeroDto;

namespace Yummy.WebUI.Validator.HeroValidationRules
{
    public class CreateHeroValidation : AbstractValidator<CreateHeroDto>
    {
        public CreateHeroValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Alanı boş bırakılamaz").
                MaximumLength(35).WithMessage("Başlık alanı maksimum 35 karakterden oluşmalıdır.").
                MinimumLength(5).WithMessage("Başlık alanı minimum 5 karakterden oluşmalıdır");

            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("AltBaşlık Alanı boş bırakılamaz").
               MaximumLength(35).WithMessage("AltBaşlık alanı maksimum 35 karakterden oluşmalıdır.").
               MinimumLength(5).WithMessage("AltBaşlık alanı minimum 5 karakterden oluşmalıdır");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Alanı boş bırakılamaz").
               MaximumLength(250).WithMessage("Açıklama alanı maksimum 250 karakterden oluşmalıdır.").
               MinimumLength(5).WithMessage("Açıklama alanı minimum 5 karakterden oluşmalıdır");
        }
    }
}
