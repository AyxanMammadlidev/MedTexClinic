using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Product>> GetProductsByPriceDescending(int page, int take);
        Task<IEnumerable<Product>> GetProductsByPriceAscending(int page, int take);
        Task<IEnumerable<Product>> GetProductsByRating(int page, int take);

    }

}
