namespace FinalProject.Application.DTOs.Department
{
    public record CreateDepartmentDto(string Name, string Description, bool status, int ChiefDoctorId);

}
