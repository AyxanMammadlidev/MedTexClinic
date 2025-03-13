using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {

        Task<Appointment?> GetAppointmentByDateAndDoctorAsync(DateTime date, int doctorId);
        Task<Appointment?> GetAppointmentByNumber(string appointmentNumber);
        Task<bool> HasPatientAppointmentForDateAsync(string PatientCode, DateTime date);
    }
}
