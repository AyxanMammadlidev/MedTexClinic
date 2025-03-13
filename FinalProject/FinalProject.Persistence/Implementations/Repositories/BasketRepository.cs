using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private readonly AppDbContext _context;
        public BasketRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Basket> GetBasketByUserIdAsync(string userId)
        {
            return await _context.Baskets.Include(x => x.Items).FirstOrDefaultAsync(b => b.UserId == userId);
        }


    }
}
