using FluentValidation;
using Yummy.WebUI.Dtos.ContactDto;

namespace Yummy.WebUI.Validator.ContactValidationRules
{
    public class CreateContactValidation : AbstractValidator<CreateContactDto>
    {
        public CreateContactValidation()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres alanı zorunludur.");
            RuleFor(x => x.OpenHours).NotEmpty().WithMessage("Çalışma Saatleri alanı zorunludur.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon numarası zorunludur.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
        }
    }
}
