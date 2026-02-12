using FluentValidation;
using Yummy.WebUI.Dtos.TestimonialDto;

namespace Yummy.WebUI.Validator.TestimonialValidationRules
{
    public class CreateTestimonialValidation : AbstractValidator<CreateTestimonialDto>
    {
        public CreateTestimonialValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş bırakılamaz")
               .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir.");

            RuleFor(x => x.Comment).NotEmpty().WithMessage("Yorum alanı boş bırakılamaz")
                .MinimumLength(20).WithMessage("Yorum en az 20 karakter olmalıdır.");

            RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("Puanlama 1 ile 5 arasında olmalıdır.");
        }
    }
}
