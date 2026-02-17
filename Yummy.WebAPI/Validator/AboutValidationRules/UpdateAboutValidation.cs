using FluentValidation;
using Yummy.WebAPI.Dtos.AboutDto;

namespace Yummy.WebAPI.Validator.AboutValidationRules
{
    public class UpdateEmployeeTaskValidation : AbstractValidator<UpdateAboutDto>
    {
        public UpdateEmployeeTaskValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Alanı boş bırakılamaz").
             MaximumLength(35).WithMessage("Başlık alanı maksimum 35 karakterden oluşmalıdır.").
             MinimumLength(5).WithMessage("Başlık alanı minimum 5 karakterden oluşmalıdır");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Alanı boş bırakılamaz").
                MaximumLength(250).WithMessage("Açıklama alanı maksimum 250 karakterden oluşmalıdır.").
                MinimumLength(5).WithMessage("Açıklama alanı minimum 5 karakterden oluşmalıdır");

            RuleFor(x => x.ReservationNumber).NotEmpty().WithMessage("Rezervasyon Numarası Alanı boş bırakılamaz").
                MaximumLength(20).WithMessage("Rezervasyon Numarası alanı maksimum 20 karakterden oluşmalıdır.").
                MinimumLength(5).WithMessage("Rezervasyon Numarası alanı minimum 5 karakterden oluşmalıdır");
        }
    }
}
