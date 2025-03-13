using FinalProject.Application.DTOs.Product;


namespace FinalProject.Application.DTOs.Category
{

    public record GetCategoryDto(string name, ICollection<GetProductDto> Products);

}
