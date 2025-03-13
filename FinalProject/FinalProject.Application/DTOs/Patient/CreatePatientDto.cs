namespace FinalProject.Application.DTOs.Patient
{
    public record CreatePatientDto(
     string Name,
     string Surname,
     DateOnly DateOfBirth,
     string Adress,
     string Phone,
     string Email);
}
