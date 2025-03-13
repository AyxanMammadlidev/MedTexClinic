namespace FinalProject.Application.DTOs.Patient
{
    public record PatientItemDto(
        string IdentityCode,
        int Id,
      string Name,
     string Surname,
     DateOnly DateOfBirth,
     string Adress,
     string Phone,
     string Email
     );

}
