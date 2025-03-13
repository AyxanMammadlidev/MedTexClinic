using AutoMapper;
using FinalProject.Application.DTOs.Comment;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.MappingProfiles
{
    internal class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateCommentDto, Comment>()
                ;

            CreateMap<UpdateCommentDto, Comment>()
                .ForMember(c => c.Id, opt => opt.Ignore());


            CreateMap<Comment, GetCommentDto>()



            .ReverseMap();

            CreateMap<Comment, CommentItemDto>();











        }
    }
}
