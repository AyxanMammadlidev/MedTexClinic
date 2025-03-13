using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Department;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        public readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _departmentService.GetAllAsync(page, take));
        }



        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            var getDepartmentDto = await _departmentService.GetByIdAsync(id);

            if (getDepartmentDto == null) return NotFound();
            return Ok(getDepartmentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateDepartmentDto departmentDto)
        {
            await _departmentService.CreateAsync(departmentDto);
            return StatusCode(StatusCodes.Status201Created);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDepartmentDto departmentDto)
        {
            if (id < 1) return BadRequest();

            await _departmentService.UpdateAsync(id, departmentDto);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            await _departmentService.DeleteAsync(id);

            return NoContent();
        }
    }
}

