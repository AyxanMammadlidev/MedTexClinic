using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Doctor> SearchChiefDoctorAsync(int chiefId)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == chiefId);
        }
    }
}
