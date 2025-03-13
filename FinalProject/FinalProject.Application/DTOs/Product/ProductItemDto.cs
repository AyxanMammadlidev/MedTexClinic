using FinalProject.Application.DTOs.ProductReview;

namespace FinalProject.Application.DTOs.Product
{
    public record ProductItemDto(
        string ImageUrl,
        int Id,
        string Name,
        string Description,
        string SKU,
        decimal Price,
        int CategoryId,
        ICollection<GetReviewDto>? Reviews);


}
