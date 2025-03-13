using FinalProject.Application.DTOs.Product;
using FluentValidation;

namespace FinalProject.Application.Validators.ProductValidators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");



            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .MaximumLength(1000).WithMessage("Description can have a maximum of 1000 characters.");

            RuleFor(p => p.SKU)
                .NotEmpty().WithMessage("SKU cannot be empty.")
                .Matches(@"^[A-Z]{2}-\d{3}$")
                .WithMessage("SKU must be in the format 'XX-123' (2 uppercase letters, a hyphen, and 3 digits).");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("A valid category must be selected.");
        }
    }
}
