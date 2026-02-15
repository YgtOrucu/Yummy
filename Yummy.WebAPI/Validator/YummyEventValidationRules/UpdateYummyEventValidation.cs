using FluentValidation;
using Yummy.WebAPI.Dtos.YummyEventsDto;

namespace Yummy.WebAPI.Validator.YummyEventValidationRules
{
    public class UpdateYummyEventValidation : AbstractValidator<UpdateYummyEventDto>
    {
        public UpdateYummyEventValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Etkinlik başlığı boş olamaz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş olamaz");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır");
        }
    }
}
