using AutoMapper;
using FinalProject.Application.DTOs.Basket;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.MappingProfiles
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>();
            CreateMap<BasketItem, BasketItemDto>();
        }
    }
}
