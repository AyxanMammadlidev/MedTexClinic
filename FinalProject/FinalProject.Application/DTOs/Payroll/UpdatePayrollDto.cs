namespace FinalProject.Application.DTOs.Payroll
{
    public record UpdatePayrollDto
        (decimal Salary,
        decimal TaxRate,
        decimal InsuranceRate);
}
