using AutoMapper;
using FinalProject.Application.DTOs.Account;
using FinalProject.Domain.Entities;




namespace FinalProject.Application.MappingProfiles
{
    internal class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<RegisterDto, AppUser>();

        }

    }
}
