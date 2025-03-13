namespace FinalProject.Application.DTOs.ProductReview
{
    public record GetReviewDto(string? Content, int ProductId, string UserId, int Rating);

}
