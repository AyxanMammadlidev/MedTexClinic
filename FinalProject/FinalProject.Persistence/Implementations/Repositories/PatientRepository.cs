using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Patient> SearchPatientIdentityAsync(string IdentityCode)
        {

            return await _context.Patients
                .FirstOrDefaultAsync(p => p.IdentityCode == IdentityCode);


        }

        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm)
        {
            return await _context.Patients
           .Where(p => p.Name.Contains(searchTerm) ||
                      p.Surname.Contains(searchTerm)).ToListAsync();



        }

    }
}
