using FluentValidation;
using Yummy.WebUI.Dtos.MessageDto;

namespace Yummy.WebUI.Validator.MessageValidationRules
{
    public class CreateMessageValidation : AbstractValidator<CreateMessageDto>
    {
        public CreateMessageValidation()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir e-posta giriniz");
            RuleFor(x => x.Subject).MinimumLength(5).WithMessage("Konu en az 5 karakterden oluşmalıdır");
            RuleFor(x => x.MessageContent).MinimumLength(10).WithMessage("Mesaj İçeriği en az 10 karakterden oluşmalıdır");
        }
    }
}
