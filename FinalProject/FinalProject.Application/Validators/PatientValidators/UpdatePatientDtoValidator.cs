using FinalProject.Application.DTOs.Patient;
using FluentValidation;

namespace FinalProject.Application.Validators.PatientValidators
{
    public class UpdatePatientDtoValidator : AbstractValidator<UpdatePatientDto>
    {
        public UpdatePatientDtoValidator()
        {
            RuleFor(p => p.Name)
         .NotEmpty().WithMessage("First name is required.")
         .Length(2, 100).WithMessage("First name must be between 2-50 characters.");

            RuleFor(p => p.Surname)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 100).WithMessage("Last name must be between 2-50 characters.");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d+$").WithMessage("Invalid phone number format.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(p => p.Adress)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");
        }
    }
}
