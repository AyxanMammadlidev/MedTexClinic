using FinalProject.Application.DTOs.Patient;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IPatientService
    {
        Task<ICollection<PatientItemDto>> GetAllAsync(int page, int take);
        Task<GetPatientDto> GetByIdAsync(int id);
        Task CreateAsync(CreatePatientDto createPatientDto);
        Task UpdateAsync(int id, UpdatePatientDto updatePatientDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetPatientDto>> SearchAsync(string searchTerm);
        Task<GetPatientDto> SearchIdentityAsync(string IdentityCode);
    }
}
