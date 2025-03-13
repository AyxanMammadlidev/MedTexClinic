namespace FinalProject.Application.DTOs.Department
{
    public record GetDepartmentDto(int Id, string Name, string Description, int? ChiefDoctorId);

}
