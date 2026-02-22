using FluentValidation;
using Yummy.WebUI.Dtos.LoginDto;

namespace Yummy.WebUI.Validator.LoginValidationRules
{
    public class LoginValidation :AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Mail adı boş bırakılamaz.").EmailAddress().WithMessage("Lütffen geçerli bir mail adresi giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş bırakılamaz.");
        }
    }
}
