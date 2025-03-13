namespace FinalProject.Application.DTOs.Payroll
{
    public record PayrollItemDto(int Id,
        int DoctorId,
        decimal Salary,
        decimal TaxRate,
        decimal InsuranceRate,
        decimal NetSalary,
        DateOnly PaymentTime);

}
