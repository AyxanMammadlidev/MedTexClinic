using AutoMapper;
using FinalProject.Application.DTOs.Product;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDto>()
                .ForCtorParam(nameof(GetProductDto.Reviews), opt => opt.MapFrom(src => src.Reviews))
                .ReverseMap();
            CreateMap<CreateProductDto, Product>();

            CreateMap<UpdateProductDto, Product>().ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<Product, ProductItemDto>()
                .ForCtorParam(nameof(ProductItemDto.Reviews), opt => opt.MapFrom(src => src.Reviews));


        }
    }
}
