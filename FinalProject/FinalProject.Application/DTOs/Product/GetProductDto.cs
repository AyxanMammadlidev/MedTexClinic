using FinalProject.Application.DTOs.ProductReview;

namespace FinalProject.Application.DTOs.Product
{
    public record GetProductDto(
        string ImageUrl,
        string Name,
        string Description,
        string SKU,
        decimal Price,
        int CategoryId,
        ICollection<GetReviewDto>? Reviews);

}
