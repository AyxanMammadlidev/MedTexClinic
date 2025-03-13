using FinalProject.Application.DTOs.Doctor;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IDoctorService
    {
        Task<ICollection<DoctorItemDto>> GetAllAsync(int page, int take);
        Task<GetDoctorDto> GetByIdAsync(int id);
        Task CreateAsync(CreateDoctorDto doctorDto);
        Task UpdateAsync(int id, UpdateDoctorDto doctorDto);
        Task DeleteAsync(int id);
    }
}
