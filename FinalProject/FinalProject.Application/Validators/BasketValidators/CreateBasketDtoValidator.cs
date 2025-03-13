using FinalProject.Application.DTOs.Basket;
using FluentValidation;

namespace FinalProject.Application.Validators.BasketValidators
{
    public class CreateBasketDtoValidator : AbstractValidator<CreateBasketDto>
    {
        public CreateBasketDtoValidator()
        {
            RuleFor(x => x.Quantity).LessThanOrEqualTo(10).GreaterThan(0).NotEmpty();
        }
    }
}
