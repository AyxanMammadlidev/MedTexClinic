namespace FinalProject.Application.DTOs.Appointment
{
    public record CreateAppointmentDto(
    DateTime AppointmentDate,

    string PatientCode,
    int DoctorId
);
}
