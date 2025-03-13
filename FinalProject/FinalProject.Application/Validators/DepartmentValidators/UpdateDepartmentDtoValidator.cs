using FinalProject.Application.DTOs.Department;
using FluentValidation;

namespace FinalProject.Application.Validators.DepartmentValidators
{
    public class UpdateDepartmentDtoValidator : AbstractValidator<UpdateDepartmentDto>
    {
        public UpdateDepartmentDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100)
               .Matches(@"^[a-zA-ZəƏıİöÖüÜğĞçÇşŞ]+$");
            RuleFor(x => x.Description).NotEmpty().MinimumLength(3).MaximumLength(1000);

        }
    }
}
