using Microsoft.AspNetCore.Http;

namespace FinalProject.Application.DTOs.Product
{
    public record UpdateProductDto(
        IFormFile Photo,
        string Name,
        string SKU,
        string Description,
        decimal Price,
        int CategoryId);

}
