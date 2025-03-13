using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.ProductReview;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;


        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;

        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            var reviews = await _reviewService.GetAllAsync(page, take);
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateReviewDto reviewDto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();

            await _reviewService.CreateAsync(userId, reviewDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateReviewDto reviewDto)
        {
            if (id < 1)
                return BadRequest();
            await _reviewService.UpdateAsync(id, reviewDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            await _reviewService.DeleteAsync(id);
            return NoContent();
        }

    }
}
