using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _categoryService.GetAllAsync(page, take));
        }



        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            var categoryDto = await _categoryService.GetByIdAsync(id);

            if (categoryDto == null) return NotFound();
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto categoryDto)
        {
            await _categoryService.CreateAsync(categoryDto);
            return StatusCode(StatusCodes.Status201Created);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto categoryDto)
        {
            if (id < 1) return BadRequest();

            await _categoryService.UpdateAsync(id, categoryDto);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            await _categoryService.DeleteAsync(id);

            return NoContent();
        }
    }
}
