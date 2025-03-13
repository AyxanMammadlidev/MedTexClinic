namespace FinalProject.Application.DTOs.Patient
{
    public record GetPatientDto(
     string IdentityCode,
     string Name,
     string Surname,
     DateOnly DateOfBirth,
     string Adress,
     string Phone,
     string Email);

}
