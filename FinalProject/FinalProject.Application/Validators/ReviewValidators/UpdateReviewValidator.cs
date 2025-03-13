using FinalProject.Application.DTOs.ProductReview;
using FluentValidation;

namespace FinalProject.Application.Validators.ReviewValidators
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewValidator()
        {
            RuleFor(x => x.Content).MaximumLength(300);
            RuleFor(x => x.Rating).GreaterThan(0).LessThan(6);
        }
    }
}
