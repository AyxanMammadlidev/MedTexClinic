using Microsoft.AspNetCore.Http;

namespace FinalProject.Application.DTOs.Doctor
{
    public record CreateDoctorDto(
        IFormFile Photo,
        string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string Description,
    int DepartmentId,
    string? Twitter,
    string? Skype,
    string? Facebook,
    string? Ven
    );

}
