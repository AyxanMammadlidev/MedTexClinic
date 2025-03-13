using FinalProject.Application.DTOs.Comment;

namespace FinalProject.Application.Abstractions.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentItemDto>> GetDoctorCommentsAsync(int doctorId);
        Task<GetCommentDto> GetCommentByIdAsync(int id);
        Task CreateCommentAsync(string userId, CreateCommentDto commentDto);
        Task UpdateCommentAsync(int id, UpdateCommentDto commentDto);
        Task DeleteCommentAsync(int id);
    }
}
