using FinalProject.Application.DTOs.ProductReview;
using FluentValidation;

namespace FinalProject.Application.Validators.ReviewValidators
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewValidator()

        {
            RuleFor(x => x.Content).MaximumLength(300);
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.Rating).GreaterThan(0).LessThan(6);


        }
    }
}
