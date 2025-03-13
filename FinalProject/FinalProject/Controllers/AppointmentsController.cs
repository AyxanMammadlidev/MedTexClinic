using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAppointmentDto appointmentDto)
        {
            await _appointmentService.CreateAsync(appointmentDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateAppointmentDto appointmentDto)
        {
            if (id < 1) return BadRequest();
            await _appointmentService.UpdateAsync(id, appointmentDto);
            return NoContent();
        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _appointmentService.GetAllAsync(page, take));
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _appointmentService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            await _appointmentService.DeleteAsync(id);

            return NoContent();
        }


        [HttpPost("cancel-appointment")]

        public async Task<IActionResult> Cancel(string appointmentNumber)
        {
            await _appointmentService.CancelAppointmentAsync(appointmentNumber);
            return NoContent();
        }



        [HttpGet("Doctor-Appointments")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorAppointments(string doctorCode,DateOnly date)
        {
            var appointments = await _appointmentService.GetDoctorAppointmentsAsync(doctorCode,date);
            return Ok(appointments);
        }

    }
}
