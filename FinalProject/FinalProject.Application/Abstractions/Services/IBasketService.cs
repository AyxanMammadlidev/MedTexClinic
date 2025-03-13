using FinalProject.Application.DTOs.Basket;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string userId);
        Task AddBasketAsync(string userId, int productId, int quantity);
        Task RemoveItemAsync(string userId, int productId);
        Task DecreaseItemQuantityAsync(string userId, int productId);
        Task IncreaseItemQuantityAsync(string userId, int productId);
        Task ClearBasketAsync(string userId);

    }
}
