using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<Doctor> SearchByIdentityNumberAsync(string identityNumber,DateOnly date);
    }
}
