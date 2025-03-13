using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<Basket> GetBasketByUserIdAsync(string userId);

    }
}
