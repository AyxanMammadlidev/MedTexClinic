using FinalProject.Application.DTOs.Account;
using FluentValidation;

namespace FinalProject.Application.Validators.AccountValidators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.EmailOrUsername)
                .NotEmpty();


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 8 characters");
        }
    }
}
