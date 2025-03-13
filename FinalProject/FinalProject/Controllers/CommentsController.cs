using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using System.Security.Claims;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int doctorId)
        {
            var comments = await _commentService.GetDoctorCommentsAsync(doctorId);
            return Ok(comments);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var comment = await _commentService.GetCommentByIdAsync(id);
            return Ok(comment);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCommentDto commentDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();




            await _commentService.CreateCommentAsync(userId, commentDto);
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Update(int id, UpdateCommentDto commentDto)
        {
            if (id < 1) return BadRequest();
            await _commentService.UpdateCommentAsync(id, commentDto);
            return NoContent();
        }



        [Authorize(Roles = "Patient,Doctor,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId is null)
                    return Unauthorized();

                GetCommentDto comment = await _commentService.GetCommentByIdAsync(id);


                bool isAdmin = User.IsInRole("Admin");
                if (!isAdmin && comment.UserId != userId)
                    return Forbid();

                await _commentService.DeleteCommentAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

