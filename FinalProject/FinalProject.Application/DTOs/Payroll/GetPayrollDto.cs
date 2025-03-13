namespace FinalProject.Application.DTOs.Payroll
{
    public record GetPayrollDto(int DoctorId, decimal Salary, decimal TaxRate, decimal InsuranceRate, decimal NetSalary, DateOnly PaymentTime);

}
