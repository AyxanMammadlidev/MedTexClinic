using FinalProject.Application.DTOs.Basket;
using FluentValidation;

namespace FinalProject.Application.Validators.BasketValidators
{
    public class ItemDtoValidator : AbstractValidator<ItemDto>
    {
        public ItemDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().GreaterThan(0);
        }
    }
}
