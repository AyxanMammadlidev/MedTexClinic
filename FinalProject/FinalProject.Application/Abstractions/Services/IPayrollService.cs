using FinalProject.Application.DTOs.Payroll;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IPayrollService
    {
        Task<IEnumerable<PayrollItemDto>> GetAllAsync(int page, int take);
        Task<GetPayrollDto> GetByIdAsync(int id);
        Task CreateAsync(CreatePayrollDto payrollDto);
        Task UpdateAsync(int id, UpdatePayrollDto payrollDto);
        Task DeleteAsync(int id);
    }
}
