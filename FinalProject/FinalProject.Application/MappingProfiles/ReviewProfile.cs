using AutoMapper;
using FinalProject.Application.DTOs.ProductReview;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.MappingProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, GetReviewDto>().ReverseMap();
            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>().ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<Review, ReviewItemDto>();
        }
    }
}
