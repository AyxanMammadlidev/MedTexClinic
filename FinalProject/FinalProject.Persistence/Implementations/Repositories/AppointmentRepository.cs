using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context) : base(context)
        {

            _context = context;

        }


        public async Task<Appointment?> GetAppointmentByDateAndDoctorAsync(DateTime date, int doctorId)
        {

            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a =>
                    a.DoctorId == doctorId &&
                    a.AppointmentDate == date

                );
        }


        public async Task<bool> HasPatientAppointmentForDateAsync(string patientCode, DateTime date)
        {

            var patient = await _context.Patients
      .FirstOrDefaultAsync(p => p.IdentityCode == patientCode);
            if(patient is null) 
                return false;
            var startDate = date.Date;

            var endDate = startDate.AddDays(1).AddTicks(-1);

            return await _context.Appointments
                .AnyAsync(a =>
                    a.PatientId == patient.Id &&
                    a.AppointmentDate >= startDate &&
                    a.AppointmentDate <= endDate
                );
        }

        public async Task<Appointment?> GetAppointmentByNumber(string appointmentNumber)
        {
            Appointment? appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentNumber == appointmentNumber);
            return appointment;
        }


       
    }
}
