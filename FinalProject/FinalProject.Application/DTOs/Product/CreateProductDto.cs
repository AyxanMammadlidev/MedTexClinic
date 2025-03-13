using Microsoft.AspNetCore.Http;

namespace FinalProject.Application.DTOs.Product
{
    public record CreateProductDto(
        IFormFile Photo,
        string Name,
        string SKU,
        decimal Price,
        string Description,
        int CategoryId);

}
