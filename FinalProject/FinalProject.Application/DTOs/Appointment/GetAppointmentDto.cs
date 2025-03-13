namespace FinalProject.Application.DTOs.Appointment
{
    public record GetAppointmentDto(
     DateTime AppointmentDate,
     string AppointmentNumber,
     bool IsCanceled,
     string PatientName,
     string PatientCode,
     string PatientSurname,
     string DoctorName,
     string DoctorSurname
 );
}
