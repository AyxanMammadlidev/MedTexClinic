using FinalProject.Application.DTOs.Department;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentItemDto>> GetAllAsync(int page, int take);
        Task<GetDepartmentDto> GetByIdAsync(int id);
        Task CreateAsync(CreateDepartmentDto tagDto);
        Task UpdateAsync(int id, UpdateDepartmentDto tagDto);
        Task DeleteAsync(int id);
    }
}
