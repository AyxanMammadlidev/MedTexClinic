using FinalProject.Application.DTOs.Product;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductItemDto>> GetAllAsync(int page, int take);
        Task<GetProductDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto productDto);
        Task UpdateAsync(int id, UpdateProductDto productDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetProductDto>> FilterByPriceAsync(decimal minPrice, decimal maxPrice);
        public Task<IEnumerable<GetProductDto>> GetProductsByPriceDescending(int page, int take);
        public Task<IEnumerable<GetProductDto>> GetProductsByPriceAscending(int page, int take);
        public Task<IEnumerable<GetProductDto>> GetProductsByRating(int page, int take);
    }
}
