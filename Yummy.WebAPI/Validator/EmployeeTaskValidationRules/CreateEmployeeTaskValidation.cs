using FluentValidation;
using Yummy.WebAPI.Dtos.EmployeeTaskDto;

namespace Yummy.WebAPI.Validator.EmployeeTaskValidationRules
{
    public class CreateEmployeeTaskValidation : AbstractValidator<CreateEmployeeTaskDto>
    {
        public CreateEmployeeTaskValidation()
        {
            RuleFor(x => x.TaskName).NotEmpty().WithMessage("Task İsmi Alanı boş bırakılamaz").
            MaximumLength(50).WithMessage("Task İsmi alanı maksimum 50 karakterden oluşmalıdır.").
            MinimumLength(5).WithMessage("Task İsmi alanı minimum 5 karakterden oluşmalıdır");

            RuleFor(x => x.Priority).NotEmpty().WithMessage("Öncelik Alanı boş bırakılamaz").
                MaximumLength(15).WithMessage("Öncelik alanı maksimum 15 karakterden oluşmalıdır.").
                MinimumLength(5).WithMessage("Öncelik alanı minimum 5 karakterden oluşmalıdır");
        }
    }
}
