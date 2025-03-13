using FinalProject.Application.DTOs.Category;
using FluentValidation;

namespace FinalProject.Application.Validators.CategoryValidators
{
    internal class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(3)
                .Matches(@"^[a-zA-ZəƏıİöÖüÜğĞçÇşŞ]+$");
        }
    }
}
