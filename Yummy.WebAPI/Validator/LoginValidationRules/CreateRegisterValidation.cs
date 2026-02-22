using FluentValidation;
using Yummy.WebAPI.Dtos.LoginDto;

namespace Yummy.WebAPI.Validator.LoginValidationRules
{
    public class CreateRegisterValidation : AbstractValidator<RegisterDto>
    {
        public CreateRegisterValidation()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Ad alanı boş geçilemez.")
              .MaximumLength(30).WithMessage("Ad en fazla 30 karakter olabilir.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad alanı boş geçilemez.")
                .MaximumLength(30).WithMessage("Soyad en fazla 30 karakter olabilir.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş geçilemez.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş geçilemez.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.ImageFile)
               .NotEmpty().WithMessage("Resim boş geçilemez.");
        }
    }
}
