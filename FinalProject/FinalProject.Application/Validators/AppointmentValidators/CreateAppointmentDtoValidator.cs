using FinalProject.Application.DTOs.Appointment;
using FluentValidation;

namespace FinalProject.Application.Validators.AppointmentValidators
{
    public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentDtoValidator()
        {
            RuleFor(x => x.PatientCode)
            .NotEmpty();

            RuleFor(x => x.DoctorId)
            .NotEmpty()
            .GreaterThan(0);


            RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .Must(BeAValidFutureDate)
            .WithMessage("Appointment Cant be in the past");
        }

        private bool BeAValidFutureDate(DateTime date)
        {
            return date > DateTime.Now;
        }
    }
}
