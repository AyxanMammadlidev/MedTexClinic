using FinalProject.Application.DTOs.Category;
using FluentValidation;

namespace FinalProject.Application.Validators.CategoryValidators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100)
                .Matches(@"^[a-zA-ZəƏıİöÖüÜğĞçÇşŞ]+$");

        }
    }
}
