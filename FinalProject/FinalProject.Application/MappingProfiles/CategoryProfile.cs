using AutoMapper;
using FinalProject.Application.DTOs.Category;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.MappingProfiles
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>().ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<Category, CategoryItemDto>();
        }

    }
}
