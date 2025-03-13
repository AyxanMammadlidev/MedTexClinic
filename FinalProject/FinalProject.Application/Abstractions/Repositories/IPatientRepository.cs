using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm);
        Task<Patient> SearchPatientIdentityAsync(string IdentityCode);
    }
}
