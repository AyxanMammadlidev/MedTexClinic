using AutoMapper;
using FinalProject.Application.DTOs.Doctor;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.MappingProfiles
{
    internal class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>().ForMember(x => x.Id, opt => opt.Ignore())
                .ForSourceMember(x => x.Name, opt => opt.DoNotValidate());
            CreateMap<Doctor, GetDoctorDto>().ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.JoinDate)).ReverseMap();
            CreateMap<Doctor, DoctorItemDto>();




        }
    }
}
