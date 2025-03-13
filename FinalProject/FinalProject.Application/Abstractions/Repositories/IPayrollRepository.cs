using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.Abstractions.Repositories
{

    public interface IPayrollRepository : IRepository<Payroll>
    {
        Task<Payroll> SearchPayrollAsync(int doctorId);

    }
}
