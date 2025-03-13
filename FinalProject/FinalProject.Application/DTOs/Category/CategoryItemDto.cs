using FinalProject.Application.DTOs.Product;

namespace FinalProject.Application.DTOs.Category
{
    public record CategoryItemDto(int Id, string Name, ICollection<GetProductDto> Products);

}
