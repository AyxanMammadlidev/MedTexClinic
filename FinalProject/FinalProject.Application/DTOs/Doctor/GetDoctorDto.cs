using FinalProject.Application.DTOs.Appointment;
using FinalProject.Application.DTOs.Comment;

namespace FinalProject.Application.DTOs.Doctor
{
    public record GetDoctorDto(
        string ImageUrl,
        string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    DateOnly JoinDate,
    string Description,
    int DepartmentId,
    string? Twitter,
    string? Skype,
    string? Facebook,
    string? Ven,
    ICollection<GetCommentDto> Comments

        );

}
