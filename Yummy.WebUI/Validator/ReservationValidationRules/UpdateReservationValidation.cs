using FluentValidation;
using Yummy.WebUI.Dtos.ReservationDto;

namespace Yummy.WebUI.Validator.ReservationValidationRules
{
    public class UpdateReservationValidation : AbstractValidator<UpdateReservationDto>
    {
        public UpdateReservationValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı zorunludur.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon alanı zorunludur.");
            RuleFor(x => x.PeopleCount).GreaterThan(0).WithMessage("Kişi sayısı en az 1 olmalıdır.");
            RuleFor(x => x.ReservationDate).NotEmpty().WithMessage("Tarih seçimi zorunludur.");
            RuleFor(x => x.ReservationTime).NotEmpty().WithMessage("Saat seçimi zorunludur.");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj alanı boş geçilemez.").
               MinimumLength(10).WithMessage("Minumum 10 karekterlik bir mesaj yazınız");
        }
    }
}
