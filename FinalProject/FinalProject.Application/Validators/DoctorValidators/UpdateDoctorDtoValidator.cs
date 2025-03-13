using FinalProject.Application.DTOs.Doctor;
using FluentValidation;

namespace FinalProject.Application.Validators.DoctorValidators
{
    public class UpdateDoctorDtoValidator : AbstractValidator<UpdateDoctorDto>
    {
        public UpdateDoctorDtoValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name is required")
             .Matches(@"^[a-zA-ZəƏıİöÖüÜğĞçÇşŞ]+$")
             .MinimumLength(2).WithMessage("Name must be at least 2 characters")
             .MaximumLength(100).WithMessage("Name cannot exceed 50 characters");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required")
                .Matches(@"^[a-zA-ZəƏıİöÖüÜğĞçÇşŞ]+$")
                .MinimumLength(2).WithMessage("Surname must be at least 2 characters")
                .MaximumLength(100).WithMessage("Surname cannot exceed 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(x => x.Description)
         .NotEmpty().WithMessage("Name is required")
         .MinimumLength(5).WithMessage("Description must be at least 5 characters")
         .MaximumLength(3000).WithMessage("Description cannot exceed 3000 characters");

            RuleFor(x => x.PhoneNumber)
      .NotEmpty().WithMessage("Phone number is required")
      .Matches(@"^\+?\d+$")
      .WithMessage("Phone number can only contain digits and optionally start with +")
      .MaximumLength(20)
      .MinimumLength(7);
        }
    }
}
