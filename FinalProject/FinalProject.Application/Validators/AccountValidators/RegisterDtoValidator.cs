using FinalProject.Application.DTOs.Account;
using FluentValidation;

namespace FinalProject.Application.Validators.AccountValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(100)
                .Matches("^[a-zA-ZçğıöşüəƏÇĞİÖŞÜ]+$");
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(2).MaximumLength(100)
                .Matches("^[a-zA-ZçğıöşüəƏÇĞİÖŞÜ]+$");
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(4).MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(64);


        }
    }
}
