using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Payroll;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 3)
        {
            var patients = await _payrollService.GetAllAsync(page, take);
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _payrollService.GetByIdAsync(id);
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePayrollDto payrollDto)
        {
            await _payrollService.CreateAsync(payrollDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdatePayrollDto payrollDto)
        {
            if (id < 1)
                return BadRequest();
            await _payrollService.UpdateAsync(id, payrollDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _payrollService.DeleteAsync(id);
            return NoContent();
        }
    }
}
