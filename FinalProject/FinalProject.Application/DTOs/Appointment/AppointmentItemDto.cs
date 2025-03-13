namespace FinalProject.Application.DTOs.Appointment
{
    public record AppointmentItemDto(
     int Id,
     DateTime AppointmentDate,
     string PatientCode,
     string AppointmentNumber,
     bool IsCanceled,
     string PatientName,
     string PatientSurname,
     string DoctorName,
     string DoctorSurname
 );
}
