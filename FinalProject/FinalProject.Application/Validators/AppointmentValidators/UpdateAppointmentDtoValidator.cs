using FinalProject.Application.DTOs.Appointment;
using FluentValidation;

namespace FinalProject.Application.Validators.AppointmentValidators
{
    public class UpdateAppointmentDtoValidator : AbstractValidator<UpdateAppointmentDto>
    {
        public UpdateAppointmentDtoValidator()
        {




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
