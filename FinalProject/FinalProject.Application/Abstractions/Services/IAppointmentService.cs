using FinalProject.Application.DTOs.Appointment;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IAppointmentService
    {
        Task<GetAppointmentDto> GetByIdAsync(int id);
        Task<IEnumerable<AppointmentItemDto>> GetAllAsync(int page, int take);
        Task CreateAsync(CreateAppointmentDto appointmentDto);
        Task CancelAppointmentAsync(string appointmentNumber);
        Task UpdateAsync(int id, UpdateAppointmentDto appointmentDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetAppointmentDto>> GetDoctorAppointmentsAsync(string doctorCode,DateOnly date); 
    }
}
