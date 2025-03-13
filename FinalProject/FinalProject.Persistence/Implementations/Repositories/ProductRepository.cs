using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {

            _context = context;

        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return await _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .Include(p => p.Reviews)
                .OrderBy(p => p.Price)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceDescending(int page, int take)
        {
            return await _context.Products
                .OrderByDescending(p => p.Price)
                .Include(p => p.Reviews)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();


        }

        public async Task<IEnumerable<Product>> GetProductsByPriceAscending(int page, int take)
        {
            return await _context.Products
              .OrderBy(p => p.Price)
              .Include(p => p.Reviews)
              .Skip((page - 1) * take)
              .Take(take)
              .ToListAsync();


        }

        public async Task<IEnumerable<Product>> GetProductsByRating(int page, int take)
        {
            return await _context.Products
             .Include(p => p.Reviews)
             .OrderByDescending(p => p.Reviews.Any() ?
             p.Reviews.Average(r => r.Rating) : 0) 
             .Skip((page - 1) * take)
             .Take(take)
             .ToListAsync();

        }
    }
}
