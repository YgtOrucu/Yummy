using FluentValidation;
using Yummy.WebUI.Dtos.EmployeeTaskDto;

namespace Yummy.WebUI.Validator.EmployeeTaskValidationRules
{
    public class CreateEmployeeTaskValidator : AbstractValidator<CreateEmployeeTaskDto>
    {
        public CreateEmployeeTaskValidator()
        {
            RuleFor(x => x.TaskName).NotEmpty().WithMessage("Görev adı boş geçilemez.");
            RuleFor(x => x.TaskName).MinimumLength(5).WithMessage("Görev adı en az 5 karakter olmalıdır.");
            RuleFor(x => x.TaskStatus).InclusiveBetween(0, 100).WithMessage("Durum 0 ile 100 arasında olmalıdır.");
            RuleFor(x => x.Priority).NotEmpty().WithMessage("Lütfen bir öncelik seçiniz.");
            RuleFor(x => x.ChefIds).NotEmpty().WithMessage("En az bir şef atamanız gerekmektedir.");
        }
    }
}
