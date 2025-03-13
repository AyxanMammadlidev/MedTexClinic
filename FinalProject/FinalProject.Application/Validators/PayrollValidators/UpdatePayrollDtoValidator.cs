using FinalProject.Application.DTOs.Payroll;
using FluentValidation;

namespace FinalProject.Application.Validators.PayrollValidators
{
    public class UpdatePayrollDtoValidator : AbstractValidator<UpdatePayrollDto>
    {
        public UpdatePayrollDtoValidator()
        {
            RuleFor(x => x.Salary)
              .GreaterThan(0).WithMessage("Salary must be greater than 0.");


            RuleFor(x => x.TaxRate)
                .InclusiveBetween(0, 100).WithMessage("TaxRate must be between 0 and 100.");


            RuleFor(x => x.InsuranceRate)
                .InclusiveBetween(0, 100).WithMessage("InsuranceRate must be between 0 and 100.");
        }
    }
}
