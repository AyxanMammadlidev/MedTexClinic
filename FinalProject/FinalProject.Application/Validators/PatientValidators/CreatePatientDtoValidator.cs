using FinalProject.Application.DTOs.Patient;
using FluentValidation;

namespace FinalProject.Application.Validators.PatientValidators
{
    public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientDtoValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("First name is required.")
            .Length(2, 50).WithMessage("First name must be between 2-50 characters.");

            RuleFor(p => p.Surname)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2-50 characters.");



            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth cannot be in the future.");


            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d+$")
                .WithMessage("Phone number can only contain digits and optionally start with +")
                .MaximumLength(20)
                .MinimumLength(7);

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");


            RuleFor(p => p.Adress)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

        }
    }

}
