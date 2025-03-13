namespace FinalProject.Application.DTOs.Payroll
{
    public record CreatePayrollDto(int DoctorId, decimal Salary, decimal TaxRate, decimal InsuranceRate);
}
