using FinalProject.Application.DTOs.ProductReview;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewItemDto>> GetAllAsync(int page, int take);
        Task<GetReviewDto> GetByIdAsync(int id);
        Task CreateAsync(string userId, CreateReviewDto reviewDto);
        Task UpdateAsync(int id, UpdateReviewDto reviewDto);
        Task DeleteAsync(int id);
    }
}
