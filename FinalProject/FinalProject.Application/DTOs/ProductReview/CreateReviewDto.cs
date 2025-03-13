namespace FinalProject.Application.DTOs.ProductReview
{
    public record CreateReviewDto(string? Content, int ProductId, int Rating);

}
