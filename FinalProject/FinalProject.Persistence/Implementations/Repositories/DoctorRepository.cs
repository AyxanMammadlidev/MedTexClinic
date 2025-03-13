using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Doctor> SearchByIdentityNumberAsync(string identityNumber, DateOnly date)
        {
            DateTime targetDate = date.ToDateTime(TimeOnly.MinValue);

            return await _context.Doctors
                .Where(d => d.IdentityCode == identityNumber)
                .Select(d => new Doctor
                {
                    Id = d.Id,
                    Name = d.Name,
                    IdentityCode = d.IdentityCode,
                    Appointments = d.Appointments
                        .Where(a => a.AppointmentDate.Date <= targetDate && !a.IsCanceled)
                        .Select(a => new Appointment
                        {
                            Id = a.Id,
                            AppointmentDate = a.AppointmentDate,
                            IsCanceled = a.IsCanceled,
                            Patient = a.Patient
                        }).ToList()
                })
                .FirstOrDefaultAsync();
        }

    }
}
