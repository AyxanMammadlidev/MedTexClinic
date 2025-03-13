using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Basket;
using FinalProject.Domain.Entities;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository,
            IProductRepository productRepository
            , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }



        public async Task AddBasketAsync(string userId, int productId, int quantity)
        {
            if (!await _productRepository.AnyAsync(p => p.Id == productId))
                throw new Exception("Please enter valid ProductId");
            var existingBasket = await _basketRepository.GetBasketByUserIdAsync(userId);
            if (existingBasket == null)
            {

                existingBasket = new Basket
                {
                    UserId = userId,
                    Items = new List<BasketItem>()
                };

                existingBasket.CreatedAt = DateTime.Now;
                existingBasket.ModifiedAt = DateTime.Now;
                await _basketRepository.SaveChangesAsync();
            }


            var existingItem = existingBasket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {

                existingItem.Quantity += quantity;
                existingBasket.ModifiedAt = DateTime.Now;
            }
            else
            {

                existingBasket.Items.Add(new BasketItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
                existingBasket.ModifiedAt = DateTime.Now;
            }


            _basketRepository.Update(existingBasket);
            await _basketRepository.SaveChangesAsync();

        }

        public async Task DecreaseItemQuantityAsync(string userId, int productId)
        {
            Basket basket = await _basketRepository.GetBasketByUserIdAsync(userId);
            if (basket is null)
                throw new Exception("Basket not found");

            BasketItem item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item is null)
                throw new Exception("item not found");

            if (item.Quantity > 1)
                item.Quantity--;
            else
                basket.Items.Remove(item);

            await _basketRepository.SaveChangesAsync();
        }

        public async Task IncreaseItemQuantityAsync(string userId, int productId)
        {
            Basket basket = await _basketRepository.GetBasketByUserIdAsync(userId);
            if (basket is null)
                throw new Exception("Basket not found");

            BasketItem item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item is null)
                throw new Exception("Item not found");

            if (item.Quantity < 10)
                item.Quantity++;

            else throw new Exception("Item quantity must less than 10");

            await _basketRepository.SaveChangesAsync();
        }

        public async Task ClearBasketAsync(string userId)
        {
            Basket basket = await _basketRepository.GetBasketByUserIdAsync(userId);
            if (basket is null)
                throw new Exception("Basket not found");
            if (!basket.Items.Any())
                return;

            basket.Items.Clear();

            await _basketRepository.SaveChangesAsync();
        }


        public async Task<BasketDto> GetBasketAsync(string userId)
        {
            Basket basket = await _basketRepository.GetBasketByUserIdAsync(userId);
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task RemoveItemAsync(string userId, int productId)
        {
            var basket = await _basketRepository.GetBasketByUserIdAsync(userId);
            if (basket == null)
                throw new Exception("basket is empty");

            var itemToRemove = basket.Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove == null)
                throw new Exception("item not found");

            basket.Items.Remove(itemToRemove);
            basket.ModifiedAt = DateTime.Now;
            await _basketRepository.SaveChangesAsync();
        }
    }
}
