using FluentValidation;
using Yummy.WebAPI.Dtos.ReservationDto;

namespace Yummy.WebAPI.Validator.ReservationValidationRules
{
    public class CreateReservationValidation : AbstractValidator<CreateReservationDto>
    {
        public CreateReservationValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı zorunludur.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon alanı zorunludur.");
            RuleFor(x => x.PeopleCount).GreaterThan(0).WithMessage("Kişi sayısı en az 1 olmalıdır.");
            RuleFor(x => x.ReservationDate).NotEmpty().WithMessage("Tarih seçimi zorunludur.");
            RuleFor(x => x.ReservationTime).NotEmpty().WithMessage("Saat seçimi zorunludur.");
        }
    }
}
