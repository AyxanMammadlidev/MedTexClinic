using AutoMapper;
using FinalProject.Application.DTOs.Appointment;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.MappingProfiles
{
    internal class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, GetAppointmentDto>()
           .ForCtorParam(nameof(GetAppointmentDto.PatientName), opt => opt.MapFrom(src => src.Patient.Name))
           .ForCtorParam(nameof(GetAppointmentDto.PatientSurname), opt => opt.MapFrom(src => src.Patient.Surname))
           .ForCtorParam(nameof(GetAppointmentDto.PatientCode), opt => opt.MapFrom(src => src.Patient.IdentityCode))
           .ForCtorParam(nameof(GetAppointmentDto.DoctorName), opt => opt.MapFrom(src => src.Doctor.Name))
           .ForCtorParam(nameof(GetAppointmentDto.DoctorSurname), opt => opt.MapFrom(src => src.Doctor.Surname))
           .ReverseMap();

            CreateMap<CreateAppointmentDto, Appointment>();

            CreateMap<UpdateAppointmentDto, Appointment>().ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<Appointment, AppointmentItemDto>()
           .ForCtorParam(nameof(GetAppointmentDto.PatientName), opt => opt.MapFrom(src => src.Patient.Name))
           .ForCtorParam(nameof(GetAppointmentDto.PatientSurname), opt => opt.MapFrom(src => src.Patient.Surname))
           .ForCtorParam(nameof(GetAppointmentDto.PatientCode), opt => opt.MapFrom(src => src.Patient.IdentityCode))
           .ForCtorParam(nameof(GetAppointmentDto.DoctorName), opt => opt.MapFrom(src => src.Doctor.Name))
           .ForCtorParam(nameof(GetAppointmentDto.DoctorSurname), opt => opt.MapFrom(src => src.Doctor.Surname));

        }
    }
}
