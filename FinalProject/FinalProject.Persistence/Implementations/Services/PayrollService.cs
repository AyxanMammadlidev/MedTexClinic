using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Payroll;
using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace FinalProject.Persistence.Implementations.Services
{

    internal class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;

        public PayrollService(IPayrollRepository payrollRepository
            , IDoctorRepository doctorRepository
            , IMapper mapper)
        {
            _payrollRepository = payrollRepository;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
        }
        public async Task CreateAsync(CreatePayrollDto payrollDto)
        {
            if (!await _doctorRepository.AnyAsync(d => d.Id == payrollDto.DoctorId))
                throw new Exception("Doctor can't found");

            Payroll existPayroll = await _payrollRepository.SearchPayrollAsync(payrollDto.DoctorId);
            if (existPayroll != null)
                throw new Exception("this doctor already have a payroll");

            Payroll payroll = _mapper.Map<Payroll>(payrollDto);
            payroll.CreatedAt = DateTime.Now;
            payroll.ModifiedAt = DateTime.Now;
            DateTime nextMonth = DateTime.Now.AddMonths(1);
            payroll.PaymentTime = new DateOnly(nextMonth.Year, nextMonth.Month, 1);

            decimal tax = (payrollDto.TaxRate / 100) * payrollDto.Salary;
            decimal insurance = (payrollDto.InsuranceRate / 100) * payrollDto.Salary;
            payroll.NetSalary = payrollDto.Salary - (tax + insurance);

            await _payrollRepository.AddAsync(payroll);
            await _payrollRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Payroll payroll = await _payrollRepository.GetbyIdAsync(id);
            if (payroll is null)
                throw new NotFoundException($"Payroll with ID {id} not found.");

            _payrollRepository.Delete(payroll);
            await _payrollRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PayrollItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Payroll> payroll = await _payrollRepository.GetAll(skip: (page - 1) * take, take: take)
               .ToListAsync();

            return _mapper.Map<IEnumerable<PayrollItemDto>>(payroll);
        }

        public async Task<GetPayrollDto> GetByIdAsync(int id)
        {
            Payroll payroll = await _payrollRepository.GetbyIdAsync(id);
            if (payroll is null)
                throw new NotFoundException($"Payroll with ID {id} not found.");

            return _mapper.Map<GetPayrollDto>(payroll);
        }

        public async Task UpdateAsync(int id, UpdatePayrollDto payrollDto)
        {
            Payroll payroll = await _payrollRepository.GetbyIdAsync(id);
            if (payroll is null)
                throw new NotFoundException($"Payroll with ID {id} not found.");

            _mapper.Map(payrollDto, payroll);
            payroll.ModifiedAt = DateTime.Now;
            decimal tax = (payrollDto.TaxRate / 100) * payrollDto.Salary;
            decimal insurance = (payrollDto.InsuranceRate / 100) * payrollDto.Salary;
            payroll.NetSalary = payrollDto.Salary - (tax + insurance);
            _payrollRepository.Update(payroll);
            await _payrollRepository.SaveChangesAsync();
        }
    }
}
