using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Doctor> SearchChiefDoctorAsync(int chiefId);

    }
}
