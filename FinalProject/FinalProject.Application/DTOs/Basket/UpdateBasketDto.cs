using FinalProject.Domain.Entities;

namespace FinalProject.Application.DTOs.Basket
{

    public record UpdateBasketDto(ICollection<BasketItem> Items);

}
