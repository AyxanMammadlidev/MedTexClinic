using AutoMapper;
using FinalProject.Application.DTOs.Patient;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.MappingProfiles
{
    internal class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientDto, Patient>();
            CreateMap<UpdatePatientDto, Patient>().ForMember(x => x.Id, opt => opt.Ignore())
                .ForSourceMember(x => x.Name, opt => opt.DoNotValidate());
            CreateMap<Patient, GetPatientDto>().ReverseMap();
            CreateMap<Patient, PatientItemDto>();
        }
    }
}
